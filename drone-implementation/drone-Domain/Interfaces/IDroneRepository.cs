using drone_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace drone_Domain.Interfaces
{
    public interface IDroneRepository : IGenericRepository<Drone>
    {
        Task<List<Drone>> FetchAll();
        Task<List<Drone>> FetchAllAvailableForLoading();
    }
}
