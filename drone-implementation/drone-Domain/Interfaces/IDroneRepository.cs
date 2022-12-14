using drone_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace drone_Domain.Interfaces
{
    public interface IDroneRepository : IGenericRepository<Drone>
    {
        Task<List<Drone>> FetchAll();
        Task<Drone> GetDrone(string serialNo);
        Task<List<Drone>> FetchAllAvailableForLoading();
        Task<bool> IsAvailableForLoading(Drone drone);
        Task<Drone> LoadDrone(Drone drone, List<Medication> medications);
    }
}
