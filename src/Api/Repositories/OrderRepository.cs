using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ef_core_example;
using ef_core_example.Models;
using Microsoft.EntityFrameworkCore;

namespace GoldMarketplace.ServerAPIService.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetOldReserved();
    }

    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context)
        {

        }

        public async Task<IEnumerable<Order>> GetOldReserved()
        {
            return await _context.Set<Order>()
                                    .FromSqlRaw("Select * from test.order group by Status having max(status);")
                                    .ToListAsync();
        }
    }
}