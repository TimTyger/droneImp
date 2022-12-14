using ddrone_DataAccess;
using drone_Domain.Entities;
using drone_Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace drone_DataAccess.Repositories
{
    public class MedicationRepository : GenericRepository<Medication>, IMedicationRepository
    {

        public MedicationRepository(ApplicationContext context ) : base(context)
        {
           }

        /// <summary>
        /// //get all drones with battery level 25 and above whose state are idle or loading
        /// </summary>
        /// <returns></returns>
        public Task<decimal> GetTotalWeightOfItems(List<int> items)
        {

            try
            {
                var medications =  _context.Medications.Where(x=>items.Contains(x.Id)).Select(a=>a.Weight).ToList();

                return Task.FromResult(medications.Sum());
            }
            catch (Exception ex)
            {
                return Task.FromResult((decimal)0);

            }
        }
        
        public async Task<List<Medication>> GetMedications(List<int> items)
        {

            try
            {
                return  await _context.Medications.Where(x=>items.Contains(x.Id)).ToListAsync();

            }
            catch (Exception ex)
            {
                return null;

            }
        }
    }
}
