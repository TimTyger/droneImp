using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Dtos
{
    public class RegisterDroneDto
    {
        [Required, StringLength(maximumLength: 100, MinimumLength = 5, ErrorMessage = "Serial Number Must Be Between 5 and 100 characters")]
        public string SerialNumber { get; set; }
        [Required]
        public int ModelId { get; set; }
        [Required]
        public int StateId { get; set; }
        [Required]
        [Range(0,100,ErrorMessage ="Value should be between 0 and 100")]
        public int BatteryLevel { get; set; }
    }
}
