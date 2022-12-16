using AutoMapper;
using drone_Domain.Dtos;
using drone_Domain.Entities;
using drone_Domain.Interfaces;
using drone_implementation.Implementation.Interfaces;
using System.Net;

namespace drone_implementation.Implementation.Services
{
    public class DroneItemService:BaseService<DroneItems, BaseDto>, IDroneItemService
    {
        private decimal _maxWeightForControl;
            private readonly IConfiguration _config;
        private readonly IMedicationRepository _medicationRepository ;
        private readonly IDroneRepository _droneRepository;  
        private readonly IDroneItemRepository _droneItemRepository;
        public DroneItemService(IBaseResponse<object> baseResponse, IMedicationRepository medicationRepository , IMapper mapper, IConfiguration config, IDroneRepository droneRepository,IDroneItemRepository droneItemRepository, IGenericRepository<DroneItems> baseRepository) : base(baseResponse, mapper, baseRepository)
        {

            _droneRepository = droneRepository;
            _config = config;
            _medicationRepository = medicationRepository;
            _droneItemRepository = droneItemRepository;
            _maxWeightForControl = decimal.Parse(_config.GetSection("DroneMaxWeight").Value);

        }

        public async Task<BaseResult<object>> FetchDroneItems(string serialNo)
        {
            try
            {
                if (String.IsNullOrWhiteSpace(serialNo))
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Enter A Valid Serial Number", statusCode: HttpStatusCode.BadRequest);
                }
                var drone = await _droneRepository.GetDrone(serialNo);
                if (drone == null)
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Drone With Serial Number Does Not Exiat", statusCode: HttpStatusCode.BadRequest);

                }
                var droneItems = await _droneItemRepository.GetItems(drone.Id);
                if (droneItems == null)
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Unable To Retrieve Drone Items At This Time", statusCode: HttpStatusCode.BadRequest);
                }
                var mappedResponse = _mapper.Map<List<MedicationResp>>(droneItems);
                return await _baseResponse.Success(response: mappedResponse);

            }
            catch (Exception ex)
            {
                return await _baseResponse.InternalServerError(ex, "Error Occured - Unable To Get Drones At This Time");
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


                if (droneExist == null)
                {
                    return await _baseResponse.CustomErrorMessage(msg: "Drone With Serial Number Does Not Exist", statusCode: HttpStatusCode.BadRequest);
                }

                var medications = await _medicationRepository.GetMedications(loadDroneDto.MedicationsId);
                if (medications == null || !medications.Any())
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
                    totalWeight += incomingWeight;
                    if (droneExist.Medications.Any())
                    {
                        var existingWeight = droneExist.Medications.Select(x => x.Weight).ToList();
                        totalWeight += existingWeight.Sum();
                    }


                    if (totalWeight <= droneExist.Model.MaxWeight && totalWeight <= _maxWeightForControl)
                    {
                        var droneItems = loadDroneDto.MedicationsId.Select(x => new DroneItems
                        {
                            CreatedAt = DateTime.Now,
                            DroneId = droneExist.Id,
                            CreatedBy = "SYSTEM",
                            MedicationId = x
                        });
                        _droneItemRepository.AddRange(droneItems);

                        //set state to loaded
                        droneExist.StateId = 3;
                        droneExist.UpdatedBy = "SYSTEM";
                        droneExist.UpdatedAt = DateTime.Now;
                        _droneRepository.Update(droneExist);
                        return await _baseResponse.Success(response: "Medications Added To Drone");

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

            }
            catch (Exception ex)
            {
                droneExist.StateId = 1;
                _droneRepository.Update(droneExist);
                return await _baseResponse.InternalServerError(ex, "Error Occured - Unable To Load Drone");
            }

        }

    }
}
