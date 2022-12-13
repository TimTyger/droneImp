using ddrone_DataAccess;
using drone_Domain.Entities;
using drone_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace drone_DataAccess.Repositories
{
    public class DroneRepository : GenericRepository<Drone>, IDroneRepository
    {
        public DroneRepository(ApplicationContext context) : base(context)
        {
        }

        //get all drones
        //attach loaded items on each drone to it before returning
        public async Task<List<Drone>> FetchAll()
        {

            try
            {
                var questions = await _context.Drones.Where(x => x.IsDeleted == false).Include(x => x.Medications)
                    .ToListAsync();

                return questions;
            }
            catch (Exception ex)
            {
                return null;
            
            }

        }


        //get all drones with battery level 25 and above whose state are idle or loading
        public async Task<List<Drone>> FetchAllAvailableForLoading()
        {

            try
            {
                var questions = await _context.Drones
                    .Where(x => x.IsDeleted == false && x.BatteryLevel >= 25 && x.StateId <= 2)
                    .Include(x => x.Medications)
                    .ToListAsync();

                return questions;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}