using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace OlympicApp.Domain
{
    public class Contest
    {
        [Key]
        public int Id { get; set; }
        public string ContestName { get; set; }
        public int SportId { get; set; }
      
        public List<Match> Matches { get; set; }

        public Sport Sport { get; set; }
       

        public Contest()
        {
            Matches = new List<Match>();

        }

    }
}
