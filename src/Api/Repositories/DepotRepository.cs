using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ef_core_example;
using ef_core_example.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldMarketplace.ServerAPIService.Repositories
{
    public interface IDepotRepository : IRepository<Depot>
    {
        public Task<bool> DepotExists(Depot depot);
    }

    public class DepotRepository : Repository<Depot>, IDepotRepository
    {
        public DepotRepository(AppDbContext context) : base(context)
        {
            
        }

        public Task<bool> DepotExists(Depot depot)
        {
            return _context.Set<Depot>().AnyAsync(dep => dep.DepotId == depot.DepotId);
        }
    }
}