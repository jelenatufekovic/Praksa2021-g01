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
        private readonly IStandingsRepository _standingsRepository;

        public StandingsService(IStandingsRepository standingRepository)
        {
            _standingsRepository = standingRepository;
        }

        public async Task<List<IStandings>> GetTableByLeagueSeasonAsync(Guid guid)
        {
            return await _standingsRepository.GetTableByLeagueSeasonAsync(guid);
        }

        public async Task<string> CheckExistingClubForLeagueSeasonAsync(IStandings standings)
        {
            return await _standingsRepository.CheckExistingClubForLeagueSeasonAsync(standings);
        }

        public async Task<bool> UpdateTableFromDelete(IStandings standings)
        {
            return await _standingsRepository.UpdateTableFromDelete(standings);
        }

        public async Task<bool> CreateTableByLeagueSeasonAsync(IStandings standings)
        {
            return await _standingsRepository.CreateTableByLeagueSeasonAsync(standings);
        }

        public async Task<bool> UpdateTableForClubAsync(IStandings standings)
        {
            return await _standingsRepository.UpdateTableForClubAsync(standings);
        }

        public async Task<bool> DeleteTableByLeagueSeasonAsync(IStandings standings)
        {
            return await _standingsRepository.DeleteTableByLeagueSeasonAsync(standings);
        }

        public async Task<bool> DeleteClubTableByLeagueSeasonAsync(IStandings standings)
        {
            return await _standingsRepository.DeleteClubTableByLeagueSeasonAsync(standings);
        }
    }
}
