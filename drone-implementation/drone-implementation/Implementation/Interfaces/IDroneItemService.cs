using drone_Domain.Dtos;
using drone_Domain.Entities;

namespace drone_implementation.Implementation.Interfaces
{
    public interface IDroneItemService : IBaseService<DroneItems, BaseDto>
    {
        Task<BaseResult<Object>> LoadDrone(LoadDroneDto loadDroneDto);
        Task<BaseResult<object>> FetchDroneItems(string serialNo);
    }
}
