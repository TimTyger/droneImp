using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Dtos
{
    public class DroneResp
    {

        public DroneResp()
        {
            this.Medications = new HashSet<MedicationResp>();
        }
        public string SerialNumber { get; set; }
        public int Id { get; set; }
        public int ModelId { get; set; }
        public int StateId { get; set; }
        public decimal Weight { get; set; }
        public int BatteryLevel { get; set; }
        public virtual ICollection<MedicationResp> Medications { get; set; }

        public virtual DroneModelResp Model { get; set; }
        public virtual StateResp State { get; set; }
    }

}
