using drone_Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace drone_Domain.Interfaces
{
    public interface IMedicationRepository : IGenericRepository<Medication>
    {
        Task<decimal> GetTotalWeightOfItems(List<int> items);
        Task<List<Medication>> GetMedications(List<int> items);
    }
}
