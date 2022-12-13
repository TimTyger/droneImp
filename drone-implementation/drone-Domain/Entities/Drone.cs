  using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Entities
{
    public class Drone : BaseEntity
    {
        public Drone()
        {
            this.Medications = new HashSet<Medication>();
        }
        public string SerialNumber { get; set; }
        public int ModelId { get; set; }
        public int StateId { get; set; }
        public decimal Weight { get; set; }
        public int BatteryLevel { get; set; }
        public virtual ICollection<Medication> Medications { get; set; }



        [ForeignKey("ModelId")]
        public virtual DroneModel Model { get; set; }

        [ForeignKey("StateId")]
        public virtual State State { get; set; }


    }
}
