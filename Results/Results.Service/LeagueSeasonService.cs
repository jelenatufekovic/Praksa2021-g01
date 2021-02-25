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
    public class LeagueSeasonService : ILeagueSeasonService
    {
        private readonly ILeagueSeasonRepository _leagueSeasonRepository;

        public LeagueSeasonService(ILeagueSeasonRepository leagueSeasonRepository)
        {
            _leagueSeasonRepository = leagueSeasonRepository;
        }
        public async Task<List<ILeagueSeason>> GetLeagueSeasonIdAsync()
        {
            return await _leagueSeasonRepository.GetLeagueSeasonIdAsync();
        }

        public async Task<ILeagueSeason> GetLeagueSeasonByBothIdAsync(ILeagueSeason leagueSeason)
        {
            return await _leagueSeasonRepository.GetLeagueSeasonByBothIdAsync(leagueSeason);
        }

        public async Task<Guid> LeagueSeasonRegistrationAsync(ILeagueSeason leagueSeason)
        {
            return await _leagueSeasonRepository.LeagueSeasonRegistrationAsync(leagueSeason);
        }
    }
}
