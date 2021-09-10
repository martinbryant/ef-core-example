using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ef_core_example;
using ef_core_example.Models;

namespace GoldMarketplace.ServerAPIService.Repositories
{
    public interface IDepotRepository : IRepository<Depot>
    {
        
    }

    public class DepotRepository : Repository<Depot>, IDepotRepository
    {
        public DepotRepository(AppDbContext context) : base(context)
        {

        }
    }
}