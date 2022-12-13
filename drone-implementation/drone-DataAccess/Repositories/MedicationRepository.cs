using ddrone_DataAccess;
using drone_Domain.Entities;
using drone_Domain.Interfaces;
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

    }
}
