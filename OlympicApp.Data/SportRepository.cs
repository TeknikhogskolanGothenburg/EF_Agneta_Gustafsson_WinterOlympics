using Microsoft.EntityFrameworkCore;
using OlympicApp.Domain;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OlympicApp.Data
{
    public class SportRepository : GenericRepository<OlympicContext, Sport>
    {
        public virtual async Task<ICollection<Sport>> SportStatisticsAsync() 
        {
            var context = new OlympicContext();
            var result = await context.Sports
                .Include(c => c.Contestants)
                .Include(c => c.Contests)
                .ToListAsync();
            Console.WriteLine("Done!");
            return result;

        }

    }
}
