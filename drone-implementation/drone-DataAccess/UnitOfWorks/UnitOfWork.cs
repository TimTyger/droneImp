using ddrone_DataAccess;
using drone_DataAccess.Repositories;
using drone_Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace drone_DataAccess.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
            Drones = new DroneRepository(_context);
            Medications = new MedicationRepository(_context);
            Models = new DroneModelRepository(_context);
            States = new StateRepository(_context);
        }
        public IDroneRepository Drones { get; private set; }
        public IMedicationRepository Medications { get; private set; }

        public IStateRepository States { get; private set; }
        public IDroneModelRepository Models { get; private set; }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
