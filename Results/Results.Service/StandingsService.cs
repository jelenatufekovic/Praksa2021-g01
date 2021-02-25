using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using Results.Common.Utils.QueryParameters;
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

        public async Task<List<IStandings>> GetStandingsByLeagueSeasonAsync(Guid id)
        {
            return await _standingsRepository.GetStandingsByLeagueSeasonAsync(id);
        }

        public async Task<IStandings> GetStandingsByQueryAsync(StandingsParameters parameters)
        {
            return await _standingsRepository.GetStandingsByQueryAsync(parameters);
        }

        public async Task<bool> CheckStandingsForClubAsync(IStandings standings)
        {
            return await _standingsRepository.CheckStandingsForClubAsync(standings);
        }

        public async Task<bool> CreateStandingsForClubAsync(IStandings standings)
        {
            return await _standingsRepository.CreateStandingsForClubAsync(standings);
        }

        public async Task<bool> UpdateStandingsForClubAsync(IStandings standings)
        {
            if((standings.Played == (standings.Won + standings.Draw + standings.Lost)) && (standings.Points <= ((standings.Won*3) + standings.Draw)))
            {
                return await _standingsRepository.UpdateStandingsForClubAsync(standings);
            }
            return false;
        }

        public async Task<bool> DeleteLeagueSeasonStandingsAsync(IStandings standings)
        {
            return await _standingsRepository.DeleteLeagueSeasonStandingsAsync(standings);
        }

        public async Task<bool> DeleteClubStandingsAsync(IStandings standings)
        {
            return await _standingsRepository.DeleteClubStandingsAsync(standings);
        }
    }
}
