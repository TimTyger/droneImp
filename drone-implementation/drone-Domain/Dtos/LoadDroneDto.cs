using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Dtos
{
    public class LoadDroneDto
    {
        [Required, StringLength(maximumLength:100,MinimumLength =5,ErrorMessage ="Serial Number Must Be Between 5 and 100 characters")]
        public string SerialNumber { get; set; }

        [Required]
        public List<int> MedicationsId { get; set; }
    }
}
