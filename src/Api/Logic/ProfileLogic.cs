using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example.Models;
using GoldMarketplace.ServerAPIService.Repositories;

namespace ef_core_example.Logic
{
    public interface IProfileLogic
    {
        Task<Result<Profile, Error>> GetProfile(Guid id);

        Task<Result<Profile, Error>> GetProfile(string id);

        Task<Result<Profile, Error>> CreateProfile(MarketplaceProfile profileDto);

        Task<Result<Profile, Error>> UpdateProfile(MarketplaceProfile profileDto);

        Task<Result<IEnumerable<Order>, Error>> GetOldOrders();
    }

    public class ProfileLogic : IProfileLogic
    {
        private readonly AppDbContext _context;
        private readonly IProfileRepository _profiles;
        private readonly IOrderRepository _orders;

        public ProfileLogic(
            IProfileRepository profiles,
            IOrderRepository orders
            )
        {
            _profiles = profiles;
            _orders = orders;
        }

        public async Task<Result<Profile, Error>> CreateProfile(MarketplaceProfile profileDto)
        {
            return await Profile.Create(profileDto.Name)
                                .Tap(_profiles.Add)
                                .Tap(profile => _context.SaveChangesAsync());
        }

        public async Task<Result<Profile, Error>> UpdateProfile(MarketplaceProfile profileDto)
        {
            return await GetProfile(profileDto.Id)
                            .Bind(profile => Profile.EditName(profile, profileDto.Name))
                            .Tap(profile => _context.SaveChangesAsync());
        }

        public async Task<Result<Profile, Error>> GetProfile(Guid id)
        {
            return await _profiles.Get(id)
                            .ToResult(Errors.General.NotFound(nameof(Profile), id));
        }

        public async Task<Result<Profile, Error>> GetProfile(string id)
        {
            return Guid.TryParse(id, out Guid identifier)
                ? await GetProfile(identifier)
                : Errors.General.InvalidId(nameof(Profile), id);
        }

        public async Task<Result<IEnumerable<Order>, Error>> GetOldOrders()
        {
            return await _orders.GetOldReserved()
                            .ToResult();
        }
    }
}