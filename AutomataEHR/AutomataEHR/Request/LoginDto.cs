using System.ComponentModel.DataAnnotations;

namespace AutomataEHR.Request
{
    public class LoginDto
    {
        [Required]
        public int HospitalId { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
