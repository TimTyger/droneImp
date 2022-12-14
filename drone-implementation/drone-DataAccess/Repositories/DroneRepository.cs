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

        /// <summary>
        ///         //get all drones and attach loaded items, states and models on each drone to it before returning
        /// </summary>
        /// <returns></returns>
        public async Task<List<Drone>> FetchAll()
        {

            try
            {
                var drones = await _context.Drones
                    .Where(x => x.IsDeleted == false)
                    .Include(x => x.Medications)
                    .Include(x=>x.State)
                    .Include(x=>x.Model)
                    .ToListAsync();

                return drones;
            }
            catch (Exception ex)
            {
                return null;
            
            }

        }

        public async Task<Drone> GetDroneItems(string serialNo)
        {

            try
            {
                var drones = await _context.Drones
                    .Where(x => x.IsDeleted == false && x.SerialNumber==serialNo)
                    .Include(x => x.Medications)
                    .FirstAsync();

                return drones;
            }
            catch (Exception ex)
            {
                return null;

            }

        }
        
        public async Task<Drone> GetDrone(string serialNo)
        {

            try
            {
                var drones = await _context.Drones
                    .Where(x => x.IsDeleted == false && x.SerialNumber==serialNo)
                    .Include(x => x.Medications)
                    .Include(x => x.State)
                    .Include(x => x.Model)
                    .FirstAsync();

                return drones;
            }
            catch (Exception ex)
            {
                return null;

            }

        }

        /// <summary>
        /// //get all drones with battery level 25 and above whose state are idle or loading
        /// </summary>
        /// <returns></returns>
        public async Task<List<Drone>> FetchAllAvailableForLoading()
        {

            try
            {
                var drones = await _context.Drones
                    .Where(x => x.IsDeleted == false && x.BatteryLevel >= 25 && x.StateId <= 2)
                    .Include(x => x.Medications)
                    .ToListAsync();

                return drones;
            }
            catch (Exception ex)
            {
                return null;

            }
        }
        
        public async Task<int> GetDroneBatteryLevel(string serialNo)
        {

            try
            {
                var batteryLevel = await _context.Drones
                    .FirstAsync(x => x.IsDeleted == false && x.SerialNumber == serialNo )
                    ;

                return batteryLevel.BatteryLevel;
            }
            catch (Exception ex)
            {
                return -1;

            }
        }
        public Task<Drone> LoadDrone(Drone drone, List<Medication> medications)
        {

            try
            {
                foreach (var item in medications)
                {
                    drone.Medications.Add(item);
                }
                var newDrone = new Drone
                {
                    BatteryLevel = drone.BatteryLevel,
                    Id = drone.Id,
                    Medications = drone.Medications.Select(y => new Medication
                    {
                        Code = y.Code,
                        Name = y.Name,
                        Id = y.Id,
                        Image = y.Image,
                        Weight = y.Weight
                    }).ToList(),
                    Model = new DroneModel { Model = drone.Model.Model, Id = drone.Model.Id, MaxWeight = drone.Model.MaxWeight },
                    SerialNumber = drone.SerialNumber,
                    ModelId = drone.ModelId,
                    StateId = drone.StateId,
                    Weight = drone.Weight

                };
                _context.Drones.Update(drone);
                _context.SaveChanges();
                return Task.FromResult(newDrone);
            }
            catch (Exception ex)
            {
                return Task.FromResult(new Drone());

            }
        }


        /// <summary>
        /// checks if drone is available for loading
        /// </summary>
        /// <param name="drone"></param>
        /// <returns>true or false</returns>
        public Task<bool> IsAvailableForLoading(Drone drone)
        {

            try
            {
                if (drone.IsDeleted == false && drone.BatteryLevel >= 25 && drone.StateId <= 2 && (drone.State.Value == "Idle" || drone.State.Value == "Loading")) return Task.FromResult(true);
                else return Task.FromResult(false);
            }
            catch (Exception ex)
            {
                return Task.FromResult(false);

            }
        }


    }
}