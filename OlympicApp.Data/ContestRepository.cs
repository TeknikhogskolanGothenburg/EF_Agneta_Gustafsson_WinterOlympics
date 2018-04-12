using Microsoft.EntityFrameworkCore;
using OlympicApp.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlympicApp.Data
{
    public class ContestRepository : GenericRepository<OlympicContext, Contest>
    {
        public virtual async Task<ICollection<Contest>> GetAllAsyncStats()
        {
            var context = new OlympicContext();
            System.Console.WriteLine("Feel free to do other things while waiting for result");
            return await context.Contests
                .Include(cou => cou.Sport)
                .Include(m => m.Sport.Contestants)
                .Include(c => c.Matches)
                //.Include(e => e.Sport.)
                // .ThenInclude

                .ToListAsync<Contest>();

        }
    }
}
