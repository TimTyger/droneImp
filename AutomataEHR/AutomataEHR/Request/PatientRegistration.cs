namespace AutomataEHR.Request
{
    public class PatientRegistration:UserRegistration
    {
        public override UserType UserType()
        {
            return new UserType
            {
                userType = "Patient",
                userTypeId = 4
            };
        }

        public AlcoholQuestionnaire Alcohol { get; set; }
        public DemographyQuestionnaire Demographics { get; set; }
        public TreatmentCoverage TreatmentCoverage { get; set; }
        public HealthQuestionnaire HealthQuestionnaire { get; set; }
        public HealthHistory AdultHealthHistory { get; set; }
        public PatientHealthQuestionnaire PatientHealthQuestionnaire { get; set; }
    }
}
