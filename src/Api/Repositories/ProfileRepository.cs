using ef_core_example;
using ef_core_example.Models;

namespace GoldMarketplace.ServerAPIService.Repositories
{
    public interface IProfileRepository : IRepository<Profile>
    {

    }

    public class ProfileRepository : Repository<Profile>, IProfileRepository
    {
        public ProfileRepository(AppDbContext context) : base(context)
        {

        }
    }
}

