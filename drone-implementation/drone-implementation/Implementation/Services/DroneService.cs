using AutoMapper;
using drone_Domain.Dtos;
using drone_Domain.Entities;
using drone_Domain.Interfaces;
using drone_implementation.Implementation.Interfaces;
using System.Net;

namespace drone_implementation.Implementation.Services
{
    public class DroneService: BaseService<Drone, BaseDto>, IDroneService
    {
        private decimal _maxWeightForControl; private readonly IConfiguration _config; IDroneRepository _droneRepository; IDroneModelRepository _droneModelRepository;
        IMedicationRepository _medicationRepository;

        public DroneService(IBaseResponse<object> baseResponse,IMapper mapper, IConfiguration config,IDroneRepository droneRepository,IMedicationRepository medicationRepository, IDroneModelRepository droneModelRepository, IGenericRepository<Drone> baseRepository) : base(baseResponse, mapper, baseRepository)
        {
            
            _droneRepository = droneRepository;
            _droneModelRepository = droneModelRepository;
            _medicationRepository = medicationRepository;
            _config = config;
            _maxWeightForControl = decimal.Parse(_config.GetSection("DroneMaxWeight").Value);

        }


        public async Task<BaseResult<Object>> RegisterDrone(RegisterDroneDto registerDroneDto)
        {

            try
            {
                // check if drone with s/n already exit
                //if not register drone
                
                    var droneExist =  _baseRepository.Find(x=>x.SerialNumber ==registerDroneDto.SerialNumber).ToList();

                if (droneExist.Count>0)
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Drone Has Previously Been Registered", statusCode: HttpStatusCode.BadRequest);
                }

                var mappedQuestion = _mapper.Map<Drone>(registerDroneDto);
                mappedQuestion.CreatedAt = DateTime.Now;
                mappedQuestion.CreatedBy = "SYSTEM";

                _droneRepository.Add(mappedQuestion);

                return await _baseResponse.Success(response:null);
            }
            catch (Exception ex)
            {

                return await _baseResponse.InternalServerError(ex, "Error Occured - Could Not Register Drone");
            }

        }


        public async Task<BaseResult<Object>> LoadDrone(LoadDroneDto loadDroneDto)
        {
            var droneExist = await _droneRepository.GetDrone(loadDroneDto.SerialNumber);

            try
            {
                //check if drone exist
                //check if drone is available for loading
                // check current weight of load on drone
                //get total weight of incoming items
                //compare sum of current and incoming weight to drone maximum capacity
                //save if maximum capcity > sum
                

                if (droneExist==null)
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Drone With Serial Number Does Not Exist", statusCode: HttpStatusCode.BadRequest);
                }

                var medications = await _medicationRepository.GetMedications(loadDroneDto.MedicationsId);
                if (medications==null || !medications.Any())
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Error Occured Binding Medications To Drone", statusCode: HttpStatusCode.BadRequest);
                }

                var isAvailable = await _droneRepository.IsAvailableForLoading(droneExist);
                if (isAvailable)
                {
                    //set state to loading
                    droneExist.StateId = 2;
                    droneExist.UpdatedBy = "SYSTEM";
                    droneExist.UpdatedAt = DateTime.Now;
                    _droneRepository.Update(droneExist);


                    decimal totalWeight = 0;
                    var incomingWeight = await _medicationRepository.GetTotalWeightOfItems(loadDroneDto.MedicationsId);
                    totalWeight+= incomingWeight;
                    if (droneExist.Medications.Any())
                    {
                        var existingWeight = droneExist.Medications.Select(x => x.Weight).ToList();
                        totalWeight += existingWeight.Sum();
                    }


                    if (totalWeight<=droneExist.Model.MaxWeight && totalWeight<= _maxWeightForControl)
                    {
                        var loadDrone = await _droneRepository.LoadDrone(droneExist, medications);
                        if (loadDrone!=null && loadDrone?.StateId==2 && loadDrone?.Id != null)
                        {
                            //set state to loaded
                            droneExist.StateId = 3;
                            droneExist.UpdatedBy = "SYSTEM";
                            droneExist.UpdatedAt = DateTime.Now;
                            _droneRepository.Update(droneExist);
                            return await _baseResponse.Success(response: "Medications Added To Drone");
                        }

                    }
                    else
                    {
                        //set state to Idle
                        droneExist.StateId = 1;
                        droneExist.UpdatedBy = "SYSTEM";
                        droneExist.UpdatedAt = DateTime.Now;
                        _droneRepository.Update(droneExist);
                        return await _baseResponse.CustomErrorMessage(msg: "Weight Exceeds Maximun Limit", statusCode: HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Drone is Currently Unavailable For Loading", statusCode: HttpStatusCode.OK);
                }
                return await _baseResponse.CustomErrorMessage(msg:"Error Occured - Unable To Load Drone",statusCode:HttpStatusCode.BadRequest);

            }
            catch (Exception ex)
            {
                droneExist.StateId = 1;
                _droneRepository.Update(droneExist);
                return await _baseResponse.InternalServerError(ex, "Error Occured - Unable To Load Drone");
            }

        }

        public async Task<BaseResult<object>> FetchDrones()
        {
            try
            {
                var drones = await _droneRepository.FetchAll();
                var mappedResponse = drones.Select(x=>
                    new DroneResp
                    {
                        BatteryLevel = x.BatteryLevel,
                        Id=x.Id,
                        Medications= x.Medications.Select(y => new MedicationResp
                        {
                            Code = y.Code,
                            Name = y.Name,
                            Id = y.Id,
                            Image = y.Image,
                            Weight = y.Weight
                        }).ToList(),
                        Model=new DroneModelResp { Model = x.Model.Model,Id=x.Model.Id,MaxWeight=x.Model.MaxWeight},
                        SerialNumber=x.SerialNumber,
                        State=new StateResp
                        {
                            Id=x.State.Id,
                            Value = x.State.Value
                        },
                        ModelId = x.ModelId,
                        StateId= x.StateId,
                        Weight = x.Weight
                      
                    });
                return await _baseResponse.Success(response:mappedResponse);

            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex, "Error Occured - Unable To Get Drones At This Time");
            }


        }
        
        public async Task<BaseResult<object>> FetchAvailbleDrones()
        {
            try
            {
                var drones = await _droneRepository.FetchAllAvailableForLoading();
                var mappedResponse = _mapper.Map<List<DroneResp>>(drones);
                return await _baseResponse.Success(response:mappedResponse);

            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex, "Error Occured - Unable To Get Drones At This Time");
            }


        }

        public async Task<BaseResult<object>> FetchDroneItems(string serialNo)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(serialNo))
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Enter A Valid Serial Number", statusCode: HttpStatusCode.BadRequest);
                }
                var drones = await _droneRepository.GetDrone(serialNo);
                if (drones==null)
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Unable To Drone With The S/N At This Time", statusCode: HttpStatusCode.BadRequest);
                }
                var mappedResponse = _mapper.Map<List<MedicationResp>>(drones.Medications);
                return await _baseResponse.Success(response: mappedResponse);

            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex, "Error Occured - Unable To Get Drones At This Time");
            }


        }
        
        public async Task<BaseResult<object>> GetDroneBatteryLevel(string serialNo)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(serialNo))
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Enter A Valid Serial Number", statusCode: HttpStatusCode.BadRequest);
                }
                var drone = await _droneRepository.GetDrone(serialNo);
                if (drone==null)
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Unable To Drone With The S/N At This Time", statusCode: HttpStatusCode.BadRequest);
                }
                return await _baseResponse.Success(response: drone.BatteryLevel);

            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex, "Error Occured - Unable To Get Drones At This Time");
            }


        }


    }
}
