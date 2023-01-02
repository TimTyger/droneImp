using System.ComponentModel.DataAnnotations;

namespace AutomataEHR.Request
{
    public class OthersRegistration : UserRegistration
    {
        [Required] public int UserTypeId { get; set; }
        [Required] public string UserTypeValue { get; set; }
        public override UserType UserType()
        {
            return new UserType
            {
                userType= this.UserTypeValue,
                userTypeId = this.UserTypeId
            };
        }
    }
}
