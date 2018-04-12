using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicApp.Domain
{
    public class Contestant
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public int CountryId { get; set; }
        public int SportId { get; set; }
       
        public List<Match> Matches { get; set; }

        public Sport Sport { get; set; }
        public Country Country { get; set; }

        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public Contestant()
        {
            Matches = new List<Match>();
        }
    }
}
