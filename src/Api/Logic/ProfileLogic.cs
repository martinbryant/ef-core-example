using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example.Models;
using GoldMarketplace.ServerAPIService.Repositories;

using ProfileResult = CSharpFunctionalExtensions.Result<ef_core_example.Models.Profile, ef_core_example.Models.Error>;

namespace ef_core_example.Logic
{
    public interface IProfileLogic
    {
        Task<ProfileResult> GetProfile(Guid id);

        Task<ProfileResult> GetProfile(string id);

        Task<ProfileResult> CreateProfile(MarketplaceProfile profileDto);

        Task<ProfileResult> UpdateProfile(MarketplaceProfile profileDto);

        Task<Result<IEnumerable<Order>, Error>> GetOldOrders();
    }

    public class ProfileLogic : IProfileLogic
    {
        private readonly IProfileRepository _profiles;
        private readonly IOrderRepository _orders;
        private readonly IUnitOfWork _unit;

        public ProfileLogic(
            IProfileRepository profiles,
            IOrderRepository orders,
            IUnitOfWork unit
            )
        {
            _profiles = profiles;
            _orders = orders;
            _unit = unit;
        }

        public async Task<ProfileResult> CreateProfile(MarketplaceProfile profileDto)
        {
            return await GetProfileByName(profileDto.Name)
                                .Ensure(profile => profile is null, Errors.Profile.Exists(profileDto))
                                .Bind(_ => Profile.Create(profileDto.Name))
                                .Tap(_unit.Profiles.Add)
                                .Tap(_unit.Commit);
        }

        public async Task<ProfileResult> UpdateProfile(MarketplaceProfile profileDto)
        {
            return await GetProfile(profileDto.Id)
                            .Bind(profile => Profile.EditName(profile, profileDto.Name))
                            .Tap(_unit.Commit);
        }

        public async Task<ProfileResult> GetProfile(Guid id)
        {
            return await _profiles.Get(id)
                            .ToResult(Errors.General.NotFound(nameof(Profile), id));
        }

        public async Task<ProfileResult> GetProfile(string id)
        {
            return Guid.TryParse(id, out Guid identifier)
                ? await GetProfile(identifier)
                : Errors.General.InvalidId(nameof(Profile), id);
        }

        public async Task<ProfileResult> GetProfileByName(string name)
        {
            var profiles =  await _unit.Profiles.Find(profile => profile.Name == name);

            return profiles.FirstOrDefault();
        }

        public async Task<Result<IEnumerable<Order>, Error>> GetOldOrders()
        {
            return await _orders.GetOldReserved()
                            .ToResult();
        }
    }
}