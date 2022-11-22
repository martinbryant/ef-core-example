using System;
using System.Linq;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using ef_core_example.Models;
using GoldMarketplace.ServerAPIService.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ef_core_example.Logic
{
    public interface IDepotLogic
    {
        Task<Result<Depot, Error>> CreateDepot(DepotDto depotDto);
        Task<Result<Depot, Error>> GetDepot(Guid id);
        // Task<Result<Depot, Error>> UpdateDepot(DepotDto depotDto);
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

        public async Task<Result<Depot, Error>> CreateDepot(DepotDto depotDto)
        {
            return await _profile.GetProfile(depotDto.ProfileId)
                                    .Bind(profile => Depot.Create(depotDto, profile))
                                    .Ensure(DepotIsUnique, Errors.Depot.DepotAlreadyExists(depotDto.DepotId))
                                    .Tap(_unit.Depots.Add)
                                    .Tap(_unit.Commit);
        }

        public async Task<Result<Depot, Error>> GetDepot(Guid id)
        {
            return await _unit.Depots.Get(id)
                            .ToResult(Errors.General.NotFound(nameof(Depot), id));
        }

        public async Task<Result<Depot, Error>> UpdateDepot(Guid id, DepotDto depotDto)
        {
            return await GetDepot(id)
                            .Bind(depot => Depot.Update(depot, depotDto))
                            .Ensure(DepotIsUnique, Errors.Depot.DepotAlreadyExists(depotDto.DepotId))
                            .Tap(_unit.Commit);
        }

        private async Task<Result<Depot, Error>> GetDepot(string id)
        {
            return Guid.TryParse(id, out Guid identifier)
                ? await GetDepot(identifier)
                : Errors.General.InvalidId(nameof(Depot), id);
        }

        private async Task<bool> DepotIsUnique(Depot depot)
        {
            var exists = await _unit.Depots.AsQueryable()
                                        .AnyAsync(dep => dep.DepotId == depot.DepotId);

            return exists == false;
        }
    }
}