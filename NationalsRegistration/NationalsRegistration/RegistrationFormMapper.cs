using System;
using System.Collections.Generic;
using System.Linq;
using FileHelpers;
using NationalsRegistration.Objects;

namespace NationalsRegistration
{
    internal static class RegistrationFormMapper
    {
        /// TODO:    Add Validation (i.e. ensure no team member has duplicates, and no team has more than correct number per primary category)
        /// Add category allocations
        private const string ReadFileName = "Team-Registration (2017-01-04).csv";

        private const string WriteFileName = "MappedTeams(2017-01-04).csv";

        private static void Main()
        {
            var readerEngine = new FileHelperEngine<RegistrationFormRaw>();
            var writerEnginge = new FileHelperEngine<RegistrationFormMapped>();
            var allTeamInformation = readerEngine.ReadFile(ReadFileName);
            var titleRow = CreateTitleRow();

            writerEnginge.AppendToFile(WriteFileName, titleRow);

            foreach (var individualTeamInformation in allTeamInformation)
            {
                AddIndividualTeamToFile(individualTeamInformation, writerEnginge);
            }
            Console.ReadKey();
        }

        private static void AddIndividualTeamToFile(RegistrationFormRaw registrationFormRaw,
            IFileHelperEngine<RegistrationFormMapped> writerEnginge)
        {
            var teamEntries = registrationFormRaw.TeamEntries;

            var wordSplits = new[]
            {"Name:", "; Choice 1:", "; Choice 2:", "; Choice 3:", "; Painting Competition:", "; Notes:"};
            var nameSplit = new[] {"Name:"};
            var teamEntriesIndividuals = teamEntries.Split(nameSplit, StringSplitOptions.None);
            var teamNumber = 0;
            var currentTeamValidation = SetUpValidations();
            foreach (var individual in teamEntriesIndividuals)
            {
                if (teamNumber == 0)
                {
                }
                else
                {
                    AddTeamMemberToFile(individual, wordSplits, registrationFormRaw, writerEnginge,
                        currentTeamValidation);
                }
                teamNumber++;
            }
            foreach (var validation in currentTeamValidation.Where(validation => !validation.Valid))
            {
                Console.Write("\r\n " + registrationFormRaw.TeamName + " INVALID OPTIONS! " + validation.Name);
            }
        }

        private static void AddTeamMemberToFile(string individual, string[] wordSplits,
            RegistrationFormRaw registrationFormRaw, IFileHelperEngine<RegistrationFormMapped> writerEnginge,
            IEnumerable<CategoryValidation> currentTeamValidation)
        {
            var teamDetails = individual.Split(wordSplits, StringSplitOptions.None);

            var mappedEntry = new RegistrationFormMapped
            {
                TeamName = registrationFormRaw.TeamName,
                AffiliatedUniversity = registrationFormRaw.AffiliatedUniversity,
                TeamCaptain = registrationFormRaw.TeamCaptain,
                ContactNumber = registrationFormRaw.ContactNumber,
                ContactEmail = registrationFormRaw.ContactEmail,
                TeamMember = teamDetails[0],
                Choice1 = teamDetails[1],
                Choice2 = teamDetails[2],
                Choice3 = teamDetails[3],
                PaintingChoice = teamDetails[4],
                Notes = teamDetails[5]
            };

            //if (string.Equals(mappedEntry.Choice1, mappedEntry.Choice2) ||
            //    string.Equals(mappedEntry.Choice1, mappedEntry.Choice3) ||
            //    string.Equals(mappedEntry.Choice2, mappedEntry.Choice3)
            //    )
            //{
            //    Console.Write("\r\n "+ mappedEntry.TeamName + " INVALID OPTIONS! Duplicate choice. " +  " -- " +
            //                  mappedEntry.TeamMember);
            //}

            foreach (
                var validation in
                    currentTeamValidation.Where(
                        validation => string.Equals(validation.Name.ToLower(), teamDetails[1].ToLower().TrimStart())))
            {
                validation.CurrentNumber++;
                break;
            }
            writerEnginge.AppendToFile(WriteFileName, mappedEntry);
        }

        private static RegistrationFormMapped CreateTitleRow()
        {
            var firstRow = new RegistrationFormMapped
            {
                TeamName = "Team Name",
                AffiliatedUniversity = "University",
                TeamCaptain = "Team Captain",
                ContactNumber = "Contact Number",
                ContactEmail = "Contact Email",
                TeamMember = "Team Member",
                Choice1 = "Choice 1",
                Choice2 = "Choice 2",
                Choice3 = "Choice 3",
                PaintingChoice = "Painting Choice",
                Notes = "Notes"
            };
            return firstRow;
        }

        private static List<CategoryValidation> SetUpValidations()
        {
            var categoryValidations = new List<CategoryValidation>
            {
                new CategoryValidation
                {
                    Name = SubCategories.Action,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Anime,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.CallOfCthulhu,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.DarkFuture,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.NewWoD,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Pathfinder,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.DnD4,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.DnD5,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Diceless,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Eastern,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.FantasyOpen,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.FantasyUrban,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.GodsAndDemons,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Horror,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Homebrew,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Humour,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Indie,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Outlaws,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Paranoia,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Retro,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.SciFi,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.StarWars,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Supers,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Survival,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Systemless,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Temporal,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.TrueRandom,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Steampunk,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Warhammer40KRpg,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.WarhammerFantasyRpg,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.WeirdAndWibbly,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Western,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.WorldOfDarkness,
                    Category = Category.Rpg
                },
                new CategoryValidation
                {
                    Name = SubCategories.Bloodbowl,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.BoltAction,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.GuildBall,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.Malifaux,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.XWing,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.Warhammer40K,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.AgeOfSigmar,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.WarmaHordes,
                    Category = Category.Wargame
                },
                new CategoryValidation
                {
                    Name = SubCategories.CompetitiveBoard,
                    Category = Category.Boardgame
                },
                new CategoryValidation
                {
                    Name = SubCategories.CooperativeBoard,
                    Category = Category.Boardgame
                },
                new CategoryValidation
                {
                    Name = SubCategories.SocialBoard,
                    Category = Category.Boardgame
                },
                new CategoryValidation
                {
                    Name = SubCategories.Magic,
                    Category = Category.Cardgame
                },
                new CategoryValidation
                {
                    Name = SubCategories.Pokemon,
                    Category = Category.Cardgame
                },
                new CategoryValidation
                {
                    Name = SubCategories.Android,
                    Category = Category.Cardgame
                },
                new CategoryValidation
                {
                    Name = SubCategories.Larp,
                    Category = Category.Larp
                },
                new CategoryValidation
                {
                    Name = SubCategories.Gauntlet,
                    Category = Category.Gauntlet
                }
            };
            return categoryValidations;
        }
    }
}