using OlympicApp.Domain;
using System;
using System.Linq;

namespace OlympicApp.Data
{
    public class DbInitializer
    {
        public static void Initializer()  //KLAR
        {
            var context = new OlympicContext();
            // Looks for any sports. If Db is empty it runs below, else it skips this.
            if (context.Sports.Any())
            {
                return;   // DB has been initialized
            }

            var sport = new Sport[]
            {
            new Sport { SportName = "Alpine Skiing" },
            new Sport { SportName = "Curling" },
            new Sport { SportName = "IceHockey" },
            new Sport { SportName = "Snowboard" },
            new Sport { SportName = "Speedskating" },
            new Sport { SportName = "Nordic Combined" }
            };
            foreach (Sport d in sport)
            {
                context.Add(d);
            }
            context.SaveChanges();

            var country = new Country[]
             {
                new Country { CountryName = "Sweden", Gold = 2, Silver = 0, Bronze = 6 },
                new Country { CountryName = "Norway", Gold = 0, Silver = 0, Bronze = 0 },
                new Country { CountryName = "Germany", Gold = 0, Silver = 2, Bronze = 0 },
                new Country { CountryName = "Russia", Gold = 0, Silver = 1, Bronze = 0 },
                new Country { CountryName = "United States", Gold = 1, Silver = 0, Bronze = 0 },
                new Country { CountryName = "Great Britain", Gold = 0, Silver = 0, Bronze = 1 },
                new Country { CountryName = "Finland", Gold = 4, Silver = 0, Bronze = 0 },
                new Country { CountryName = "Japan", Gold = 5, Silver = 1, Bronze = 0 },
                new Country { CountryName = "France", Gold = 0, Silver = 0, Bronze = 1 }
             };

            foreach (Country d in country)
            {
                context.Add(d);
            }
            context.SaveChanges();

            var contest = new Contest[]
            {
             new Contest { ContestName = "Alpine combined men", SportId = sport.Single( s => s.SportName == "Alpine Skiing").Id },
             new Contest { ContestName = "Giant slalom women", SportId = sport.Single( s => s.SportName == "Alpine Skiing").Id },
             new Contest { ContestName = "Super-G men", SportId = sport.Single( s => s.SportName == "Alpine Skiing").Id },
             new Contest { ContestName = "50km men", SportId = sport.Single( s => s.SportName == "Nordic Combined").Id },
             new Contest { ContestName = "4x5km relay women", SportId = sport.Single( s => s.SportName == "Nordic Combined").Id },
            new Contest { ContestName = "Sprint 1,5km men", SportId = sport.Single( s => s.SportName == "Nordic Combined").Id },
             new Contest { ContestName = "Curling men ", SportId = sport.Single( s => s.SportName == "Curling").Id }
        };
            foreach (Contest d in contest)
            {
                context.AddRange(d);
            }
            context.SaveChanges();

            var referee = new Referee[]
            {
             new Referee { Name = "Tobias Wehrli", CountryId = country.Single(s => s.CountryName == "Norway").Id },
             new Referee { Name = "Nathan Vanoosten", CountryId = country.Single(s => s.CountryName == "Finland").Id },
             new Referee { Name = "Dina Allen", CountryId = country.Single(s => s.CountryName == "United States").Id },
             new Referee { Name = "Gabriella Gran", CountryId = country.Single(s => s.CountryName == "Sweden").Id },
             new Referee { Name = "Mark Lemelin", CountryId = country.Single(s => s.CountryName == "United States").Id },
             new Referee { Name = "Roman Gofman", CountryId = country.Single(s => s.CountryName == "Germany").Id }
        };
            foreach (Referee d in referee)
            {
                context.AddRange(d);
            }
            context.SaveChanges();
            var contestant = new Contestant[]
            {
            new Contestant { FirstName = "Will", LastName = "Smith", Age = 26, Gender= "male", CountryId = country.Single(s => s.CountryName == "United States").Id, SportId = sport.Single(s => s.SportName == "IceHockey").Id },
            new Contestant { FirstName = "Feng", LastName = "Shui", Age = 28, Gender= "female",CountryId = country.Single(s => s.CountryName == "Japan").Id, SportId = sport.Single(s => s.SportName == "Nordic Combined").Id },
            new Contestant { FirstName = "Nina", LastName = "Ollinen", Age = 16,Gender= "female", CountryId = country.Single(s => s.CountryName == "Finland").Id, SportId = sport.Single(s => s.SportName == "Alpine Skiing").Id },
            new Contestant { FirstName = "James", LastName = "Bond", Age = 36,Gender= "male", CountryId = country.Single(s => s.CountryName == "Great Britain").Id, SportId = sport.Single(s => s.SportName == "Alpine Skiing").Id },
            new Contestant { FirstName = "Melinda", LastName = "Jones", Age = 20,Gender= "female", CountryId = country.Single(s => s.CountryName == "United States").Id, SportId = sport.Single(s => s.SportName == "Alpine Skiing").Id },
            new Contestant { FirstName = "Tryggve", LastName = "Aasheim", Age = 18,Gender= "male", CountryId = country.Single(s => s.CountryName == "Norway").Id, SportId = sport.Single(s => s.SportName == "Nordic Combined").Id },
            new Contestant { FirstName = "Jochen", LastName = "Thewes", Age = 19,Gender= "male", CountryId = country.Single(s => s.CountryName == "Germany").Id, SportId = sport.Single(s => s.SportName == "Alpine Skiing").Id },
            new Contestant { FirstName = "Pjotr", LastName = "Golikov", Age = 24,Gender= "male", CountryId = country.Single(s => s.CountryName == "Russia").Id, SportId = sport.Single(s => s.SportName == "Alpine Skiing").Id },
            new Contestant { FirstName = "Didier", LastName = "Ravault", Age = 27, Gender= "male",CountryId = country.Single(s => s.CountryName == "France").Id, SportId = sport.Single(s => s.SportName == "Nordic Combined").Id }
        };
            foreach (Contestant d in contestant)
            {
                context.AddRange(d);
            }
            context.SaveChanges();
            var match = new Match[]
            {
             new Domain.Match { Arena = "Forest tour", DateTime = new DateTime(2018, 05, 13), ContestId = contest.Single(s => s.ContestName == "Sprint 1,5km men").Id, RefereeId = referee.Single(s => s.Name == "Tobias Wehrli").Id, ContestantId = contestant.Single(s => s.LastName  ==  "Ravault" && s.FirstName=="Didier").Id},
            new Domain.Match { Arena = "Death Hill", DateTime = new DateTime(2018, 05, 14), ContestId = contest.Single(s => s.ContestName == "50km men").Id, RefereeId = referee.Single(s => s.Name == "Nathan Vanoosten").Id, ContestantId = contestant.Single(s => s.LastName  == "Aasheim" && s.FirstName=="Tryggve").Id },
            new Domain.Match { Arena = "Killer Slope", DateTime = new DateTime(2018, 05, 15), ContestId = contest.Single(s => s.ContestName == "Giant slalom women").Id, RefereeId = referee.Single(s => s.Name == "Dina Allen").Id, ContestantId = contestant.Single(s => s.LastName  == "Golikov" && s.FirstName=="Pjotr").Id},
            new Domain.Match { Arena = "The Wall", DateTime = new DateTime(2018, 05, 16), ContestId = contest.Single(s => s.ContestName == "Alpine combined men").Id, RefereeId = referee.Single(s => s.Name == "Roman Gofman").Id, ContestantId = contestant.Single(s => s.LastName  == "Jones" && s.FirstName=="Melinda").Id}
        };
            foreach (Match d in match)
            {
                context.AddRange(d);
            }
            context.SaveChanges();
        }
    }
}
