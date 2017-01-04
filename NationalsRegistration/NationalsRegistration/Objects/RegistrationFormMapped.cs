using FileHelpers;

namespace NationalsRegistration.Objects
{
    [DelimitedRecord(",")]
    public class RegistrationFormMapped
    {
        public string TeamName;
        public string AffiliatedUniversity;
        public string TeamCaptain;
        public string ContactNumber;
        public string ContactEmail;
        public string TeamMember;
        public string Choice1;
        public string Choice2;
        public string Choice3;
        public string PaintingChoice;
        public string Notes;
    }
}