using Results.Common.Utils.QueryParameters;
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
    public class StatisticsService : IStatisticsService
    {
        IStatisticsRepository _statisticsRepository;

        public StatisticsService(IStatisticsRepository statisticsRepository)
        {
            _statisticsRepository = statisticsRepository;
        }

        public async Task<bool> CreateStatisticsAsync(IStatistics statistics)
        {
            return await _statisticsRepository.CreateStatisticsAsync(statistics);
        }

        public async Task<IStatistics> GetStatisticsAsync(Guid MatchId)
        {
            return await _statisticsRepository.GetStatisticsAsync(MatchId);
        }

        public async Task<List<IStatistics>> GetAllStatisticsAsync(StatisticsParameters parameters)
        {
         return await _statisticsRepository.GetStatisticsByQueryAsync(parameters);
        }

        public async Task<bool> UpdateStatisticsAsync(IStatistics statistics)
        {
            return await _statisticsRepository.UpdateStatisticsAsync(statistics);
        }

        public async Task<bool> DeleteStatisticsAsync(Guid MatchId)
        {
            return await _statisticsRepository.DeleteStatisticsAsync(MatchId);
        }
    }
}
