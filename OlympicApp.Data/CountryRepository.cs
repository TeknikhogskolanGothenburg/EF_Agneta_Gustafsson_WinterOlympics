using Microsoft.EntityFrameworkCore;
using OlympicApp.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OlympicApp.Data
{
    public class CountryRepository : GenericRepository<OlympicContext, Country>
    {
    
        public virtual async Task<List<Country>> AsyncSelect() //KLAR
        {
            var context = new OlympicContext();
            var result = await context.Countries
                .OrderByDescending(m => m.Gold)
                .ThenByDescending(m => m.Silver)
                .ThenByDescending(m => m.Bronze)
                .ToListAsync();
            Console.WriteLine("Up do date!");
            return result;

        }
     
    }
}
