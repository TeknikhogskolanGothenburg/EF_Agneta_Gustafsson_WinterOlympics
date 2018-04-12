using Microsoft.EntityFrameworkCore;
using OlympicApp.Data;
using OlympicApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicApp.UI
{
    public class UpdateMethod
    {

        // Uppdaterar medaljantalet per land.
        internal static void UpdateMedals(List<Country> todaysWinners)  //KLAR
        {
            var couRep = new CountryRepository();
            var co = couRep.GetAll();
            foreach (var c in co)
            {
                foreach (var t in todaysWinners)
                {
                    if (c.CountryName == t.CountryName)
                    {
                        c.Gold = c.Gold + t.Gold;
                        c.Silver = c.Silver + t.Silver;
                        c.Bronze = c.Bronze + t.Bronze;
                    }
                }
            }
            couRep.UpdateRange(co);
            couRep.Save();
        }

        //Uppdaterar data i many to many tabell. Hade fungerat bäst med dropdowns i en websida, men i brist på det
        //använder jag consolen.
        internal static void UpdateManyToMany() //KLAR
        {
            int counter = 0;

            var mRep = new MatchRepository();
            var all = mRep.GetAll();
            foreach (var x in all)
            {
                counter++;
                Console.WriteLine("Row " + counter + ":\t" + "ContestId " + x.ContestId + "\t" + "ContestantId " + x.ContestantId + "\t" + "Arena " + x.Arena + "\t" + "Date " + x.DateTime + "\t" + "RefereeId " + x.RefereeId);
            }
            Console.Write("Which row do you want to change?\n Enter its keys, ContestId and ContestantId.\nIf you want to change keys, you have to delete the row and add a new match.\nContestId: ");

            string conid = Console.ReadLine();
            int conid_ = int.Parse(conid);
            var filterBy = mRep.FindBy(m => m.ContestId.Equals(conid_));
            foreach (var x in filterBy)
            {
                counter++;
                Console.WriteLine("ContestId: " + x.ContestId + "\t" + "ContestantId: " + x.ContestantId + "\t" + "Arena: " + x.Arena + "\t" + "Date: " + x.DateTime + "\t" + "RefereeId: " + x.RefereeId);
            }
            Console.WriteLine("Here are all contestants registered to this match.");
            Console.Write("Now choose ContestantId: ");
            string contid = Console.ReadLine();
            int contid_ = int.Parse(contid);

            Console.Write("New Arena: ");
            string arena_ = Console.ReadLine();

            Console.Write("New Date (yyyy-mm-dd): ");
            string datum_ = Console.ReadLine();
            DateTime enteredDate = DateTime.Parse(datum_);

            var reRep = new RefereeRepository();
            Console.Write("\nRefereeId: ");
            string refid = Console.ReadLine();
            int refid_ = int.Parse(refid);

            var updateMatch = mRep.FindBy(m => m.ContestId == conid_ && m.ContestantId == contid_);
            foreach (var x in updateMatch)
            {
                x.Arena = arena_;
                x.DateTime = enteredDate;
                x.RefereeId = refid_;
            }
            mRep.UpdateRange(updateMatch);
            mRep.Save();
        }

        //Uppdaterar data i grundtabeller och one to many. Använder consolen i brist på web.
        internal static void UpdateBasicData(string parameter) //KLAR
        {
            var spRep = new SportRepository();
            var alls = spRep.GetAll();
            var coRep = new CountryRepository();
            var allc = coRep.GetAll();

            if (parameter == "Contestant")
            {
                var mRep = new ContestantRepository();
                var all = mRep.GetAll();
                foreach (var x in all)
                {
                    Console.WriteLine("Id " + x.Id + ":\t" + "FirstName: " + x.FirstName 
                        + "\t" + "LastName: " + x.LastName + "\t" + "Age " + x.Age + "\t" + "Gender " 
                        + x.Gender + "\t" + "CountryId " + x.CountryId + "\t" + "SportId " + x.SportId);
                }
                int id = UpdateId();
                var change = mRep.FindBy(m => m.Id.Equals(id));
                foreach (var x in change)
                {
                    Console.Write("Enter new FirstName: ");
                    x.FirstName = Console.ReadLine();
                    Console.Write("Enter new LastName: ");
                    x.LastName = Console.ReadLine();
                    Console.Write("Enter new Age: ");
                    x.Age = int.Parse(Console.ReadLine());
                    Console.Write("Enter new Gender (male/female): ");
                    x.Gender = Console.ReadLine();
                    foreach (var y in allc)
                    {
                        Console.WriteLine(y.Id + " = " + y.CountryName);
                    }
                    
                    Console.Write("Enter new CountryId: ");
                    x.CountryId = int.Parse(Console.ReadLine());
                    foreach (var y in alls)
                    {
                        Console.WriteLine(y.Id + " = " + y.SportName);
                    }
                    Console.Write("Enter new SportId: ");
                    x.SportId = int.Parse(Console.ReadLine());
                    mRep.Update(x);
                    mRep.Save();
                }

            }
            if (parameter == "Contest")
            {
                var mRep = new ContestRepository();
                var all = mRep.GetAll();
                foreach (var x in all)
                {
                    Console.WriteLine("Id " + x.Id + ":\t" + "ContestName: " + x.ContestName + "\t\t" + "SportId " + x.SportId);
                }
                int id = UpdateId();
                var change = mRep.FindBy(m => m.Id.Equals(id));
                foreach (var x in change)
                {
                    Console.Write("Enter new ContestName: ");
                    x.ContestName = Console.ReadLine();
                    foreach (var y in alls)
                    {
                        Console.WriteLine(y.Id + " = " + y.SportName);
                    }
                    Console.Write("Enter new SportId: ");
                    x.SportId = int.Parse(Console.ReadLine());
                    mRep.Update(x);
                    mRep.Save();
                }

            }
            if (parameter == "Sport")
            {                
                foreach (var x in alls)
                {
                    Console.WriteLine("Id " + x.Id + ":\t" + "SportName: " + x.SportName);
                }
                int id = UpdateId();
                
                var change = spRep.FindBy(m => m.Id.Equals(id));
                foreach(var x in change)
                {
                    Console.Write("Enter new SportName: ");
                    x.SportName = Console.ReadLine();
                    spRep.Update(x);
                    spRep.Save();
                }
            }
            if (parameter == "Country")
            {
                
                foreach (var x in allc)
                {
                    Console.WriteLine("Id " + x.Id + ":\t" + "CountryName " + x.CountryName + "\t\t" + "Gold " + x.Gold + "\t" + "Silver " + x.Silver + "\t" + "Bronze " + x.Bronze);
                }
                int id = UpdateId();
                var change = coRep.FindBy(m => m.Id.Equals(id));
                foreach (var x in change)
                {
                    Console.Write("Enter new CountryName: ");
                    x.CountryName = Console.ReadLine();               
                    Console.Write("Enter new Gold: ");
                    x.Gold = int.Parse(Console.ReadLine());
                    Console.Write("Enter new Silver: ");
                    x.Silver = int.Parse(Console.ReadLine());
                    Console.Write("Enter new Bronze: ");
                    x.Bronze = int.Parse(Console.ReadLine());
                    coRep.Update(x);
                    coRep.Save();
                }
            }
            if (parameter == "Referee")
            {
                var mRep = new RefereeRepository();
                var all = mRep.GetAll();
                foreach (var x in all)
                {
                    Console.WriteLine("Id " + x.Id + ":\t" + "Name: " + x.Name + "\t\t" + "CountryId " + x.CountryId);
                }
                int id = UpdateId();
                var change = mRep.FindBy(m => m.Id.Equals(id));
                foreach (var x in change)
                {
                    Console.Write("Enter new Name: ");
                    x.Name = Console.ReadLine();
                    foreach (var y in allc)
                    {
                        Console.WriteLine(y.Id + " = " + y.CountryName);
                    }
                    Console.Write("Enter new CountryId: ");
                    x.CountryId = int.Parse(Console.ReadLine());
                    mRep.Update(x);
                    mRep.Save();
                }
            }
        }


        //Hör ihop med metoden ovan.
        private static int UpdateId()
        {
            Console.Write("Which id do you want to update? Enter number: ");
            string id = Console.ReadLine();
            int id_ = int.Parse(id);
            return id_;
        }
    }

}
