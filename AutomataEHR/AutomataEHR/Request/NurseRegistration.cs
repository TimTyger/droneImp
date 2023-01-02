namespace AutomataEHR.Request
{
    public class NurseRegistration :UserRegistration
    {
        public override UserType UserType()
        {
            return new UserType
            {
                userType = "Nurse",
                userTypeId = 3
            };
        }
    }
}
