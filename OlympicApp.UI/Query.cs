using Microsoft.EntityFrameworkCore;
using OlympicApp.Data;
using OlympicApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicApp.UI
{
    public class Query
    {
      
        internal static void StactisticsForContests()//KLAR 
        {
            var cRep = new ContestRepository();

            var stats = cRep.GetAllAsyncStats();

            Console.WriteLine("waiting for results");
            foreach (var entry in stats.Result)
            {
                Console.WriteLine(entry.ContestName);
                Console.WriteLine("\tTotal Participants: " + entry.Sport.Contestants.Count);

                foreach (var x in entry.Sport.Contestants)
                {
                    Console.WriteLine("\t\tContestants: " + x.FullName);
                }
            }
        }

        internal static void StatisticsForEachSport() //KLART
        {
            var cRep = new SportRepository();
            var stats = cRep.SportStatisticsAsync();
            var mRep = new ContestantRepository();
            var stat = mRep.GetAllAsync();

          
            var totalContestants = 0;

            foreach (var x in stats.Result)
            {
                int contestants = x.Contestants.Count;
                totalContestants += contestants;                  
            }
            Console.WriteLine("Statistics for each Sport: \n");
            foreach (var y in stats.Result)
            {
                int male = 0;
                int female = 0;
                int sportid = y.Id;
                int x = y.Contestants.Count;
                int contests = y.Contests.Count();
                int match = 0;
                double percent =  x*100/totalContestants;
                foreach(var z in stat.Result.Where(s => s.Gender.StartsWith("male") && s.SportId==sportid))
                {
                    male = z.FirstName.Count();
                }
                foreach (var z in stat.Result.Where(s => s.Gender.StartsWith("female") && s.SportId == sportid))
                {
                    female = z.FirstName.Count();
                }
                foreach (var z in stat.Result.Where(s => s.SportId == sportid))
                {
                    match = z.Matches.Count();
                }
                Console.WriteLine("{0} has {1} % of the participants", y.SportName, percent);                
                Console.WriteLine("{0} has {1} male and {2} female contestants. ", y.SportName, male, female);
                Console.WriteLine("{0} has {1} contests. ", y.SportName, contests);
                Console.WriteLine("{0} has {1} scheduled matches.\n ", y.SportName, match);
            }


        }
        internal static void MedalStatus()  //KLAR
        {
            var cRep = new CountryRepository();
            var result = cRep.AsyncSelect();
            Console.WriteLine("waiting for medalresults");
            foreach (var country in result.Result)
            {
                Console.WriteLine(country.CountryName);
                Console.WriteLine("\tGoldmedals: " + country.Gold);
                Console.WriteLine("\tSilvermedals: " + country.Silver);
                Console.WriteLine("\tBronzemedals: " + country.Bronze);
            }
        }


    }
}
