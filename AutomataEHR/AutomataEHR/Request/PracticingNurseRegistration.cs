namespace AutomataEHR.Request
{
    public class PracticingNurseRegistration:UserRegistration
    {
        public override UserType UserType()
        {
            return new UserType
            {
                userType = "Practicing Nurse",
                userTypeId = 2
            };
        }
    }
}
