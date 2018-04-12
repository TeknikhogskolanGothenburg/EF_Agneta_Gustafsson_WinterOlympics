using OlympicApp.Data;
using OlympicApp.Domain;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OlympicApp.UI
{
    public class DeleteMethod
    {


        //Tar bort ett id och dess barn. Förutom i två tabeller, där jag gjorde fel i migreringen och inte lyckades rätta till det. 
        // Se kommentarer på rad 81 i OlympicContext.
        internal static void DeleteBasicData(string olympicModel, string contestantFirstName, string contestantLastName, string country, string sport, string contestName, string refereeName)
        {
            var mRep = new MatchRepository();
            

            if (olympicModel == "Country")
            {
                try                         // Denna nödlösning för jag misslyckades att ändra Restict till Cascade i min sista migration.
                {
                var cRep = new CountryRepository();
                var co = cRep.FindBy(c => c.CountryName == country);
                cRep.DeleteRange(co);
                cRep.Save();
                return;
                }
                catch
                {
                    var match = mRep.FindBy(m => m.Contestant.Country.CountryName.Equals(country));
                    foreach(var x in match)
                    {
                        Console.WriteLine("Match with keyvaluepairs: "+ x.ContestId +" - " + x.ContestantId +" has to be deleted first.");
                    }
                    return;
                }
            }
            if (olympicModel == "Sport")
            {
                var cRep = new SportRepository();
                var co = cRep.FindBy(c => c.SportName == sport);
                cRep.DeleteRange(co);
                cRep.Save();
                return;
            }
            if (olympicModel == "Referee")
            {
                var cRep = new RefereeRepository();
                var co = cRep.FindBy(c => c.Name == refereeName);
                cRep.DeleteRange(co);
                cRep.Save();
                return;
            }
            if (olympicModel == "Contest")
            {
                var cRep = new ContestRepository();
                var co = cRep.FindBy(c => c.ContestName == contestName);
                cRep.DeleteRange(co);
                cRep.Save();
                return;
            }
            if (olympicModel == "Contestant")
            {
               
                try                          // Denna nödlösning för jag misslyckades att ändra Restict till Cascade i min sista migration. (Tror jag i alla fall)
                {
                var cRep = new ContestantRepository();
                var co = cRep.FindBy(c => c.FirstName == contestantFirstName && c.LastName == contestantLastName);
                cRep.DeleteRange(co);
                cRep.Save();
                return;

                }
                catch
                {
                    var match = mRep.FindBy(m => m.Contestant.FirstName.Equals(contestantFirstName) && m.Contestant.LastName.Equals(contestantLastName));
                    foreach (var x in match)
                    {
                        Console.WriteLine("Match with keyvaluepairs: " + x.ContestId + " - " + x.ContestantId + " has to be deleted first.");
                        
                    }
                    return;
                }
            }
            else
            {
                System.Console.WriteLine("{0} does not exist in database.", olympicModel);
            }


        }
        // Tar bort alla från en angiven gren, eller tar bort en deltagare från en specifik match
        internal static void DeleteManyToMany(string model, string contestantFirst, string contestantLast, string contest) //KLAR
        {
            var mRep = new MatchRepository();

            if (model == "Contest")
            {
                var mat = mRep.FindBy(m => m.Contest.ContestName.Equals(contest) );
                mRep.DeleteRange(mat);
                mRep.Save();
            }
            if (model == "Contestant")
            {
                var mat = mRep.FindBy(m => m.Contestant.FirstName.Equals(contestantFirst) && m.Contestant.LastName == contestantLast && m.Contest.ContestName == contest);
                foreach (var m in mat)
                {
                    mRep.Delete(m);
                    mRep.Save();
                }
            }
        }
    }
}
