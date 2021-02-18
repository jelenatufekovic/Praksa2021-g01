using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service
{
    public class StandingsService : IStandingsService
    {
        private readonly IStandingsRepository _standingRepository;

        public StandingsService(IStandingsRepository standingRepository)
        {
            _standingRepository = standingRepository;
        }

        public async Task<List<IStandingsModel>> GetTableByLeagueSeasonAsync(Guid guid)
        {
            return await _standingRepository.GetTableByLeagueSeasonAsync(guid);
        }

        public async Task<bool> CheckExistingClubForLeagueSeasonAsync(IStandingsModel standings)
        {
            return await _standingRepository.CheckExistingClubForLeagueSeasonAsync(standings);
        }

        public async Task<bool> CreateTableByLeagueSeasonAsync(IStandingsModel standings)
        {
            return await _standingRepository.CreateTableByLeagueSeasonAsync(standings);
        }
    }
}
