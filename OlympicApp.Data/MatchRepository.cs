using Microsoft.EntityFrameworkCore;
using OlympicApp.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicApp.Data
{
    public class MatchRepository : GenericRepository<OlympicContext, Match> 
    {
       
        public virtual async Task<ICollection<Match>> GetAllAsyncStatistics()
        {
            var context = new OlympicContext();
            return await context.Matches
                .Include(c => c.Contest.Sport)
                .OrderByDescending(m => m.Contest)
                .Include(m => m.Contest)
                .Include(cou => cou.Contestant)
                .Include(s => s.Contestant.Country)
                .ToListAsync<Match>();
        }
    }
}
