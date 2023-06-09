﻿using drone_Domain.Dtos;
using drone_Domain.Entities;

namespace drone_implementation.Implementation.Interfaces
{
    public interface IDroneService : IBaseService<Drone, BaseDto>
    {
        Task<BaseResult<Object>> RegisterDrone(RegisterDroneDto registerDroneDto);
        Task<BaseResult<object>> FetchDrones();
        Task<BaseResult<object>> FetchAvailbleDrones();
        Task<BaseResult<object>> GetDroneBatteryLevel(string serialNo);
    }
}
