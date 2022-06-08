using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example.Models;
using GoldMarketplace.ServerAPIService.Repositories;

namespace ef_core_example.Logic
{
    public interface IDepotLogic
    {
        // Task<Result<Depot, Error>> CreateDepot(MarketplaceDepot depotDto);
        Task<Result<Depot, Error>> GetDepot(Guid id);
        // Task<Result<Depot, Error>> UpdateDepot(MarketplaceDepot depotDto);
    }

    public class DepotLogic : IDepotLogic
    {
        private readonly IUnitOfWork _unit;
        private readonly IProfileLogic _profile;

        public DepotLogic(IUnitOfWork unit, IProfileLogic profile)
        {
            _unit = unit;
            _profile = profile;
        }

        // public async Task<Result<Depot, Error>> CreateDepot(MarketplaceDepot depotDto)
        // {
        //     return await _profile.GetProfile(depotDto.ProfileId)
        //         .Bind(profile => Depot.Create(depotDto, profile))
        //         .Tap(_unit.Depots.Add)
        //         .Check(_unit.Commit);
        // }

        public async Task<Result<Depot, Error>> GetDepot(Guid id)
        {
            return await _unit.Depots.Get(id)
                            .Ensure(depot =>
                                        depot != null,
                                        Errors.General.NotFound(nameof(Depot), id));
        }

        // public async Task<Result<Depot, Error>> UpdateDepot(MarketplaceDepot depotDto)
        // {
        //     return await GetDepot(depotDto.Id)
        //                     .Check(depot => Depot.EditDepot(depot, depotDto))
        //                     .Check(_unit.Commit);
        // }

        private async Task<Result<Depot, Error>> GetDepot(string id)
        {
            return Guid.TryParse(id, out Guid identifier)
                ? await GetDepot(identifier)
                : Errors.General.InvalidId(nameof(Depot), id);
        }
    }
}