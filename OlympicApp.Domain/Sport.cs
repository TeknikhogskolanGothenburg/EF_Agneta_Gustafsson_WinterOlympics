 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OlympicApp.Domain
{
    public class Sport
    {
        public int Id { get; set; }
        public string SportName { get; set; }

        public List<Contestant> Contestants { get; set; }
        public List<Contest> Contests { get; set; }

        public Sport()
        {
            Contests = new List<Contest>();
            Contestants = new List<Contestant>();
        }
    }
}
