using ddrone_DataAccess;
using drone_Domain.Entities;
using drone_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_DataAccess.Repositories
{
    public class DroneItemRepository: GenericRepository<DroneItems>, IDroneItemRepository
    {
        public DroneItemRepository(ApplicationContext context) : base(context)
        {
        }

        public async Task<List<Medication>> GetItems(int droneId)
        {
            try
            {
                var droneItems = await _context.DroneItems
                    .Where(x => x.DroneId == droneId)
                    .Include(x => x.Medication)
                    .ToListAsync();
                return droneItems.Select(x => new Medication
                {
                    Code = x.Medication.Code,
                    Name = x.Medication.Name,
                    CreatedAt = x.Medication.CreatedAt,
                    Image = x.Medication.Image,
                    Weight = x.Medication.Weight,
                    Id = x.Medication.Id,
                    CreatedBy = x.Medication.CreatedBy,
                    IsDeleted = x.Medication.IsDeleted,
                    UpdatedAt = x.Medication.UpdatedAt,
                    UpdatedBy = x.Medication.UpdatedBy
                }).ToList();
            }
            catch (Exception ex)
            {

                return null;
            }
        }
    }
}
