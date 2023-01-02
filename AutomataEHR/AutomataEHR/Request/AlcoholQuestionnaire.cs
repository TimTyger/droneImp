using System.ComponentModel.DataAnnotations;

namespace AutomataEHR.Request
{
    public class AlcoholQuestionnaire
    {
        [Required] public bool FeltNeedToCutDown { get; set; } = false;
        [Required] public bool PeopleCriticizeDrinking { get; set; } = false;
        [Required] public bool FeltGuilty { get; set; } = false;
        [Required] public bool EyeOpener { get; set; } = false;
    }
}