using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicApp.Domain
{
   public class Country
    {
        [Key]
        public int Id { get; set; }
        public string CountryName { get; set; }
        public int? Gold { get; set; }
        public int? Silver { get; set; }
        public int? Bronze { get; set; }

        public List<Referee> Referees { get; set; }
        public List<Contestant> Contestants { get; set; }


        public Country()
        {
            Referees = new List<Referee>();
            Contestants = new List<Contestant>();
        }

    }
}
