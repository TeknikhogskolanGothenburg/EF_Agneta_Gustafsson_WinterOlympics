using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OlympicApp.Domain
{
    public class Match
    {
        public int ContestantId { get; set; }
        public Contestant Contestant { get; set; }
        public int ContestId { get; set; }
        public Contest Contest { get; set; }
        public int RefereeId { get; set; }
        public Referee Referee { get; set; }
       
        public DateTime DateTime { get; set; }
        public string Arena { get; set; }

    }
}
