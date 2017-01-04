using FileHelpers;

namespace NationalsRegistration.Objects
{
    [IgnoreFirst]
    [IgnoreEmptyLines]
    [DelimitedRecord(",")]
    public class RegistrationFormRaw
    {
        public string TeamName;
        public string AffiliatedUniversity;
        public string TeamCaptain;
        public string ContactNumber;
        public string ContactEmail;
        public string TeamEntries;
    }
}