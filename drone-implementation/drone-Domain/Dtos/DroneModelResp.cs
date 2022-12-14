using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Dtos
{
    public class DroneModelResp
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public decimal MaxWeight { get; set; }
    }
}
