using ddrone_DataAccess;
using drone_Domain.Entities;
using drone_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_DataAccess.Repositories
{
    public class DroneModelRepository : GenericRepository<DroneModel>, IDroneModelRepository
    {
        public DroneModelRepository(ApplicationContext context) : base(context)
        {
        }

    }
}
