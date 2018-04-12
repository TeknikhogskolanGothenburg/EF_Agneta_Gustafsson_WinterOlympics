using OlympicApp.Data;
using System;
using System.Linq;
using OlympicApp.Domain;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace OlympicApp.UI
{
     class Program
    {
        //Dokumentation:
        //Över lag har det gått bra. Jag gjorde lite designförändringar från min testversion till denna. Men när jag upptäckte ytterligare brister
        //har jag rättat till några via migrationer och några lyckades jag inte lösa via kod. Dessa har jag dokumenterat i "OlympicContext". Jag tänkte
        //att fokus här var inte att göra en perfekt databas, utan att kommunicera med db. Hade detta varit till mitt jobb, hade jag valt att göra en
        //ny version istället, för att få det så rätt som möjligt från början.
        //I valet mellan Threading och Async, valde jag Async, för att jag bara använt anropen till att hämta befintlig data - inte för att göra 
        //processorintensiva beräkningar.
        //I valet mellan LINQ to Enities och Entity SQL, valde jag LINQ to Enities. Tyckte att urvalsfunktionerna var bättre i denna.
        //Det finns därför inga Threadings- och Entity SQL i denna kod. Om du hade önskat detta får du sätta det som komplettering, så gör jag det
        //i efterhand.
        //Jag valde att använda Generic repositories, som du visade på en av föreläsningarna. Funkade väldigt bra när allt väl var satt. Väldigt smidigt.
        //Dock stod det inte klart för mig vad jag gjorde när jag satte upp det, skrev mest av. Nu när jag använt det förstår jag mer hur och varför det 
        //används, men behöver ytterligare övning för att få den där "aha, här skulle det vara perfekt att använda IGenerics.."-insikten. Antar att det kommer
        //med tiden.
        //Valde bort att lägga tid på att få till ett riktigt User Interface. Använder input i Main samt från consolen som ska motsvara det användaren anger.

        static void Main(string[] args)
        {
            // Förser databasen med lite grunddata. Försökte lösa detta med GenericRepository, men lyckades inte. Använder därför "Using ()....."
            using (var context = new OlympicContext())  //KLAR
            {
                try
                {
                    DbInitializer.Initializer();
                }
                catch
                {
                    Console.WriteLine("An error occurred when updating database with basic info.");
                }
            }

            AddMethod.AddManyToMatch();
            AddMethod.AddContestantToMatch();

            // Formulär till metoderna: AddBasicData och DeleteBasicData
            string olympicModel = "Referee";
            string contestantFirstName = "Tryggvea";
            string contestantLastName = "Aasheim";
            int age = 20;
            string gender = "female";
            string country = "Great Britain";
            string sport = "Biathlon";
            string contestName = "1km men";
            string refereeName = "Dina Allen";


            AddMethod.AddBasicData(olympicModel, contestantFirstName, contestantLastName, age, gender, country, sport, contestName, refereeName);
            DeleteMethod.DeleteBasicData(olympicModel, contestantFirstName, contestantLastName, country, sport, contestName, refereeName);

            // Formulär till metoderna: AddManyToMany och DeleteManyToMany
            string deleteParameter = "Contestant";
            string contestantFirst = "Ninaa";
            string contestantLast = "Ollinen";
            string contest = "10km women";
            string country1 = "United States";
            string refereeFullName = "Nath Vanoosten";
            string arena = "Get fit lane";
            DateTime dateTime = new DateTime(2018, 06, 20);

            AddMethod.AddManyToMany(contestantFirst, contestantLast, contest, refereeFullName, arena, dateTime, country1);
            DeleteMethod.DeleteManyToMany(deleteParameter, contestantFirst, contestantLast, contest);

            // Formulär till metoden: UpdateBasicData
            string updateParameter = "Referee"; //Update choises: Contest, Contestant, Arena, Date, Referee, Sport
            UpdateMethod.UpdateBasicData(updateParameter);

            UpdateMethod.UpdateManyToMany();

            // Formulär till metoden: UpdateMedals
            List<Country> todaysWinners = new List<Country>();
            todaysWinners.Add(new Country() { CountryName = "Sweden", Gold = 1, Silver = 0, Bronze = 3 });
            todaysWinners.Add(new Country() { CountryName = "Finland", Gold = 1, Silver = 0, Bronze = 0 });
            todaysWinners.Add(new Country() { CountryName = "United States", Gold = 1, Silver = 2, Bronze = 0 });
            todaysWinners.Add(new Country() { CountryName = "Germany", Gold = 2, Silver = 0, Bronze = 0 });

            UpdateMethod.UpdateMedals(todaysWinners);

            Query.StactisticsForContests();
            Query.StatisticsForEachSport();
            Query.MedalStatus();
        }
    }
}

