using ef_core_example;
using ef_core_example.Models;

namespace GoldMarketplace.ServerAPIService.Repositories
{
    public interface IListingRepository : IRepository<Listing>
    {

    }

    public class ListingRepository : Repository<Listing>, IListingRepository
    {
        public ListingRepository(AppDbContext context) : base(context)
        {

        }
    }
}