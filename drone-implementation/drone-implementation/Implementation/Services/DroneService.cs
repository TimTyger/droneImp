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
        IDroneRepository _droneRepository;
        

        public DroneService(IBaseResponse<object> baseResponse,IMapper mapper, IConfiguration config,IDroneRepository droneRepository,IMedicationRepository medicationRepository, IDroneModelRepository droneModelRepository, IGenericRepository<Drone> baseRepository) : base(baseResponse, mapper, baseRepository)
        {
            _droneRepository = droneRepository;
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
