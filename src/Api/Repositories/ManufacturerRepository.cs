using ef_core_example;
using ef_core_example.Models;

namespace GoldMarketplace.ServerAPIService.Repositories
{
    public interface IManufacturerRepository : IRepository<Manufacturer>
    {

    }

    public class ManufacturerRepository : Repository<Manufacturer>, IManufacturerRepository
    {
        public ManufacturerRepository(AppDbContext context) : base(context)
        {

        }
    }
}