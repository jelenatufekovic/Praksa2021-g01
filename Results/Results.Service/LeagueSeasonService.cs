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
        public async Task<List<ILeagueSeason>> GetAllLeagueSeasonIdAsync()
        {
            return await _leagueSeasonRepository.GetAllLeagueSeasonIdAsync();
        }

        public async Task<ILeagueSeason> GetLeagueSeasonByBothIdAsync(ILeagueSeason leagueSeasonModel)
        {
            List<ILeagueSeason> list = await _leagueSeasonRepository.GetAllLeagueSeasonIdAsync();
            ILeagueSeason model = null;
            try
            {
                model = list.Where(x => x.LeagueID == leagueSeasonModel.LeagueID && x.SeasonID == leagueSeasonModel.SeasonID).FirstOrDefault();

            }
            catch (ArgumentNullException)
            {
                return null;
            }

            return model;
        }

        public async Task<Guid> LeagueSeasonRegistrationAsync(ILeagueSeason leagueSeasonModel)
        {
            return await _leagueSeasonRepository.LeagueSeasonRegistrationAsync(leagueSeasonModel);
        }
    }
}
