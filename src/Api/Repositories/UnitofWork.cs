using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example;
using ef_core_example.Models;

namespace GoldMarketplace.ServerAPIService.Repositories
{
    public interface IUnitOfWork
    {
        IProfileRepository Profiles { get; }

        IDepotRepository Depots { get; }
        IOrderRepository Orders { get; }

        Task Commit();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IProfileRepository _profileRepository;

        private IDepotRepository _depotRepository;
        private IOrderRepository _orderRepository;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        //Do assignments to the private fields here 
        public IProfileRepository Profiles
        {
            get 
            {
                if(_profileRepository == null)
                {
                    _profileRepository = new ProfileRepository(_context);
                }
                return _profileRepository;
            }
        }

        public IDepotRepository Depots
        {
            get 
            {
                if(_depotRepository == null)
                {
                    _depotRepository = new DepotRepository(_context);
                }
                return _depotRepository;
            }
        }
        public IOrderRepository Orders
        {
            get
            {
                if(_orderRepository == null)
                {
                    _orderRepository = new OrderRepository(_context);
                }
                return _orderRepository;
            }
        }



        public async Task Commit() =>
            await _context.SaveChangesAsync();
    }
}
