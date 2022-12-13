using System;
using System.Collections.Generic;
using System.Text;

namespace drone_Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IDroneRepository Drones { get;}
        IMedicationRepository Medications { get; }

       IStateRepository States { get; }
       IDroneModelRepository Models { get; }
        int Complete();
    }
}
