using drone_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Interfaces
{
    public interface IDroneItemRepository:IGenericRepository<DroneItems>
    {
        Task<List<Medication>> GetItems(int droneId);
    }
}
