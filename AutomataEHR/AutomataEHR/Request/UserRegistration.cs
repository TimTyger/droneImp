using System.ComponentModel.DataAnnotations;

namespace AutomataEHR.Request
{
    public class UserRegistration
    {

        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(15,MinimumLength = 6, ErrorMessage = "You must specify password between 6 and 15 characters")]
        public string Password { get; set; }
        [Required]
        [Compare("Password", ErrorMessage = "Password does not match!")]
        public string ConfirmPassword { get; set; }
        
        [Required(ErrorMessage = "FirstName is Required")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "You must specify a name between 3 and 15 characters")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "FirstName is Required")]
        //[StringLength(50, MinimumLength = 3, ErrorMessage = "You must specify a name between 3 and 15 characters")]
        public string LastName { get; set; }

        public virtual UserType UserType() {
            return new UserType
            {
                userTypeId = 1,
                userType = "Doctor"
            };
        }
    }
}
