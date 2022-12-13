using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Entities
{
    public class DroneModel : BaseEntity
    {
        public string Model { get; set; }
        public decimal MaxWeight { get; set; }
    }
}
