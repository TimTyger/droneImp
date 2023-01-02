using System.ComponentModel.DataAnnotations;

namespace AutomataEHR.Request
{
    public class DemographyQuestionnaire
    {
        public string City { get; set; }
        public int StateId { get; set; }
        public string ZipCode { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string SSN { get; set; }
        [Required, Phone]public string HomePhoneNumber { get; set; }
        [Required, Phone]public string CellPhoneNumber { get; set; }
        public char Gender { get; set; }
        public int BestFormOfContact { get; set; }
        public bool LeaveVoiceMessage { get; set; }
        public string PreferredPharmacy { get; set; }
        public string StreetAddress { get; set; }
        public string Apt { get; set; }
        public string EmeergencyContact { get; set; }
        public string EmergencyContactPhone { get; set; }
        public string Relationship { get; set; }
        public string AboutUs { get; set; }
        //public string PreferredPharmacy { get; set; }
        //public string PreferredPharmacy { get; set; }
    }
}