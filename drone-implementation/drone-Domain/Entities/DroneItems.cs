using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Entities
{
    public class DroneItems:BaseEntity
    {
        public int DroneId { get; set; }
        public int MedicationId { get; set; }

        [ForeignKey("MedicationId")]
        public virtual Medication Medication { get; set; }
    }
}
