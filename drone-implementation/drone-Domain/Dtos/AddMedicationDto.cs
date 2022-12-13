using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace drone_Domain.Dtos
{
    public class AddMedicationDto
    {
        [Required(AllowEmptyStrings =false,ErrorMessage ="Enter Valid Input(only Aa-Zz,0-9, - and _ are allowed)")]
        [RegularExpression(@"(^([A-Za-z0-9-_ ]*$)")]
        public string Name { get; set; }
        [Required]
        public decimal Weight { get; set; }
        [Required, RegularExpression(@"(^([A-Z0-9_]*$)")]
        public string Code { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
