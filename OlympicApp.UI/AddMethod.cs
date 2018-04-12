using OlympicApp.Data;
using OlympicApp.Domain;
using System;
namespace OlympicApp.UI
{
    public class AddMethod
    {

        // Lägger till Basic data samt till one to many tabeller.
        // För övrigt gäller samma kommentarer som till metoden under.
        internal static void AddBasicData(string olympicModel, string contestantFirstName, string contestantLastName, int age,
                                        string gender, string country, string sport, string contestName, string refereeName)  //KLAR
        {
            int sportID = 0;
            int countryID = 0;
            int contestID = 0;
            int refereeID = 0;
            int contestantID = 0;

            var conRep = new ContestantRepository();
            var conId = conRep.FindBy(m => m.FirstName.Equals(contestantFirstName) && m.LastName.Equals(contestantLastName));
            foreach (var c in conId)
            {
                contestantID = c.Id;
            }
            var cRep = new CountryRepository();
            var cId = cRep.FindBy(m => m.CountryName.StartsWith(country));
            foreach (var c in cId)
            {
                countryID = c.Id;
            }

            var sRep = new SportRepository();
            var sId = sRep.FindBy(m => m.SportName.Equals(sport));
            foreach (var c in sId)
            {
                sportID = c.Id;
            }
            var coRep = new ContestRepository();
            var coId = coRep.FindBy(m => m.ContestName.Equals(contestName));
            foreach (var c in coId)
            {
                contestID = c.Id;
            }
            var reRep = new RefereeRepository();
            var reId = reRep.FindBy(m => m.Name.Equals(refereeName));
            foreach (var c in reId)
            {
                refereeID = c.Id;
            }
            try
            {
                if (olympicModel == "Country" && countryID == 0)
                {
                    var addCountry = new CountryRepository();
                    addCountry.Add(new Country { CountryName = country });
                    addCountry.Save();
                }
            }
            catch
            {
                Console.WriteLine("{0} already exists in db.", olympicModel);
            }
            try
            {

                if (olympicModel == "Sport" && sportID == 0)
                {
                    var addSport = new SportRepository();
                    addSport.Add(new Sport { SportName = sport });
                    addSport.Save();
                }
            }
            catch
            {
                Console.WriteLine("{0} already exists in db.", olympicModel);
            }
            try
            {
                if (olympicModel == "Contest" && contestID == 0 && sportID !=0)
                {
                    var addContest = new ContestRepository();
                    addContest.Add(new Contest { ContestName = contestName, SportId = sportID });
                    addContest.Save();
                }
                else if(olympicModel == "Contest" && contestID != 0)
                {
                    Console.WriteLine("{0} already exists in db.", olympicModel);
                }
            }
            catch
            {
                Console.WriteLine("{0} is not added yet. You have to register OlympicModel: {1}, first.", olympicModel, sport);
            }
            try
            {
                if (olympicModel == "Contestant"&& contestantID ==0 && sportID != 0 && countryID != 0)
                {
                    var addContestant = new ContestantRepository();
                    addContestant.Add(new Contestant { FirstName = contestantFirstName, LastName = contestantLastName, Age = age, CountryId = countryID, Gender = gender, SportId = sportID });
                    addContestant.Save();
                }
                else if (olympicModel == "Contestant" && contestantID != 0)
                {
                    Console.WriteLine("{0} already exists in db.", olympicModel);
                }
            }
            catch
            {
                Console.WriteLine("This sport or country is not added yet. Select OlympicModel: Sport/Country, first.");
            }
            try
            {
                if (olympicModel == "Referee" && refereeID == 0 && countryID !=0)
                {
                    var addReferee = new RefereeRepository();
                    addReferee.Add(new Referee { Name = refereeName, CountryId = countryID });
                    addReferee.Save();
                    Console.WriteLine("Data is now added!");
                }
            }
            catch
            {
                Console.WriteLine("This country is not added yet. Select OlympicModel: Country, first.");
                Console.ReadKey();
            }
            if (olympicModel == "Match")
            {
                Console.WriteLine("This method doesen't work for many to many. Select AddManyToMany(), instead.");
            }
        }

        //Lägger till many to many baserat på tänkt input från Gui, i detta fall manuell input från Program.
        //i verkligt scenario skulle jag valt dropdowns från UI/Web som hämtar querys från db baserat på tidigare dropdown val
        // så att användaren aldrig kan välja alternativ som inte finns/uppfyller db-kriterier.
        // i detta exempel blir minsta felstavning fel och har varit väldigt tröttsamt att testa igenom, då jag inte har alla id:n i huvudet.....
        internal static void AddManyToMany(string contestantFirst, string contestantLast, string contest, string refereeName,
                                                                        string arena, DateTime dateTime, string country)
        {
            int refereeID = 0;
            int contestantID = 0;
            int contestID = 0;
            int sportId = 0;
            int sportID = 0;
            var refRep = new RefereeRepository();
            var coRep = new ContestantRepository();
            var conRep = new ContestRepository();
            var mRep = new MatchRepository();
            var re = refRep.FindBy(r => r.Name.EndsWith(refereeName)); // lärdom: endswith funkar inte när man länkar hela strängen....
            var refe = refRep.GetAll();
            var con = conRep.FindBy(c => c.ContestName.Equals(contest));
            var co = coRep.FindBy(m => m.FirstName.Equals(contestantFirst) && m.LastName.Equals(contestantLast));

            foreach (var c in co)
            {
                contestantID = c.Id;
                sportId = c.SportId;
            }
            foreach (var c in con)
            {
                contestID = c.Id;
                sportID = c.SportId;
            }
            foreach (var c in re)
            {
                if (refereeName == c.Name)
                {
                    refereeID = c.Id;
                }
            }

            var ma = mRep.GetAll();
            foreach (var c in ma)
            {
                try  //kollar att arenan inte dubbelbokas
                {
                    if (arena == c.Arena && c.DateTime == dateTime && c.ContestId != contestID)
                    {
                    }
                }
                catch
                {
                    Console.WriteLine("This arena is already booked for another event at this date. Please correct input.");
                }
            }

            if (contestantID == 0 || contestID == 0 || sportId != sportID ||refereeID == 0)
            {
                Console.WriteLine("Some info didn't match.\nContestantid: {0}\nContestid: {1}\nSportidn: {2} and {3} has to be the same.\nRefereeId: {4}", contestantID, contestID, sportId, sportID, refereeID);
                Console.WriteLine("\n\nIf refereeId is 0, ({0}), choose a referee from below list:", refereeName);
                foreach (var x in refe)
                {
                    Console.WriteLine(x.Name);
                }
            }                          
            else
            {

            try  // lägger till en spelare till en match
            {
                var addContestantToMatch = new MatchRepository();
                addContestantToMatch.Add(new Match { ContestantId = contestantID, ContestId = contestID, Arena = arena, DateTime = dateTime, RefereeId = refereeID });
                addContestantToMatch.Save();
            }
            catch
            {
                Console.WriteLine("{0} {1} is already participating in {2}.", contestantFirst, contestantLast, contest);
            }
            }
        }

        //enklare variant , Lägger till deltagare, om man har tillgång till alla id:n i db.
        internal static void AddContestantToMatch()//KLAR
        {
            var arena = "Forest lane";
            var date = new DateTime(2018, 2, 19);
            var contestId = 3;
            var contestantId = 42;
            // var sportId = 111;
            var mRep = new MatchRepository();
            mRep.Add(new Domain.Match { DateTime = date, Arena = arena, ContestId = contestId, ContestantId = contestantId, RefereeId = 5 });
            mRep.Save();


        }
        //Lägger till flera deltagare till samma match.
        internal static void AddManyToMatch()//KLAR
        {
            int sportId = 6;
            string gender = "female";
            var mRep = new MatchRepository();
            var coRep = new ContestantRepository();

            var participants = coRep.FindBy(m => m.SportId.Equals(sportId) && m.Gender.Equals(gender));

            foreach (var competer in participants)
            {
                mRep.Add(new Domain.Match
                {
                    DateTime = new DateTime(2018, 2, 16),
                    Arena = "summer ",
                    ContestId = 5,
                    ContestantId = competer.Id,
                    RefereeId = 6
                });
            }
            mRep.Save();

        }
    }

}



