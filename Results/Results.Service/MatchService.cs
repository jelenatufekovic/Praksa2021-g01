using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Repository;

namespace Results.Service
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IRepositoryFactory _repositoryFactory;

        public MatchService(IMatchRepository matchRepository, IRepositoryFactory repositoryFactory)
        {
            _matchRepository = matchRepository;
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Guid> CreateMatchAsync(IMatch match)
        {
            return await _matchRepository.CreateMatchAsync(match);
        }
        public async Task<bool> CheckMatchExistingAsync(MatchParameters parameters)
        {
            return await _matchRepository.CheckMatchExistingAsync(parameters);
        }
        public async Task<bool> DeleteMatchAsync(Guid id, Guid ByUser)
        {
            return await _matchRepository.DeleteMatchAsync(id, ByUser);
        }
        public async Task<bool> UpdateMatchAsync(IMatch match)
        {
            return await _matchRepository.UpdateMatchAsync(match);
        }
        public async Task<IMatch> GetMatchByIdAsync(Guid id)
        {
            return await _matchRepository.GetMatchByIdAsync(id);
        }
        public async Task<List<IMatch>> GetMatchByQueryAsync(MatchQueryParameters parameters)
        {            
            List<IMatch> matches = null;
            PagedList<IMatch> matchesList = await _matchRepository.GetMatchByQueryAsync(parameters);
            
            if(matchesList.Count > 0)
            {
                matches = matchesList.ToList();
                foreach (IMatch match in matches)
                {
                    IStatisticsRepository statisticsRepository = _repositoryFactory.GetRepository<StatisticsRepository>();
                    IStatistics statistics = await statisticsRepository.GetStatisticsAsync(match.Id);
                    if(statistics != null)
                    {
                        match.HomeGoals = statistics.HomeGoals;
                        match.AwayGoals = statistics.AwayGoals;
                    } 
                }
            }

            return matches;
        }
    }
}
