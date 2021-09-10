using System;
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
    }

    public class ProfileLogic : IProfileLogic
    {
        private readonly IUnitOfWork _unit;

        public ProfileLogic(IUnitOfWork unit)
        {
            _unit = unit;
        }

        public async Task<Result<Profile, Error>> CreateProfile(MarketplaceProfile profileDto)
        {
            return await Profile.Create(profileDto.Name)
                            .Tap(_unit.Profiles.Add)
                            .Check(_unit.Commit);
        }

        public async Task<Result<Profile, Error>> UpdateProfile(MarketplaceProfile profileDto)
        {
            return await GetProfile(profileDto.Id)
                            .Check(profile => Profile.EditName(profile, profileDto.Name))
                            .Check(_unit.Commit);
        }

        public async Task<Result<Profile, Error>> GetProfile(Guid id)
        {
            return await _unit.Profiles.Get(id)
                            .Ensure(profile => 
                                        profile != null, 
                                        Errors.General.NotFound(nameof(Profile), id));
        }
        
        public async Task<Result<Profile, Error>> GetProfile(string id)
        {
            return Guid.TryParse(id, out Guid identifier)
                ? await GetProfile(identifier)
                : Result.Failure<Profile, Error>(Errors.General.InvalidId(nameof(Profile), id));
        }
    }
}