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

        IManufacturerRepository Manufacturers { get; }

        IListingRepository Listings { get; }

        ITransactionRepository Transactions { get; }

        Task<Result<TEntity, Error>> Commit<TEntity>(TEntity entity);
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;

        private IProfileRepository _profileRepository;

        private IDepotRepository _depotRepository;

        private IManufacturerRepository _manufacturerRepository;

        private IListingRepository _listingRepository;

        private ITransactionRepository _transactionRepository;


        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IProfileRepository Profiles => _profileRepository ?? new ProfileRepository(_context);

        public IDepotRepository Depots => _depotRepository ?? new DepotRepository(_context);

        public IManufacturerRepository Manufacturers => _manufacturerRepository ?? new ManufacturerRepository(_context);

        public IListingRepository Listings => _listingRepository ?? new ListingRepository(_context);

        public ITransactionRepository Transactions => _transactionRepository ?? new TransactionRepository(_context);


    
        public async Task<Result<TEntity, Error>> Commit<TEntity>(TEntity entity) => 
            await _context.SaveChangesAsync()
                    .ToResult(entity);
    }
}