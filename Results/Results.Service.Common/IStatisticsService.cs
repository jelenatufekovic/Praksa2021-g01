using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface IStatisticsService
    {
        Task<bool> CreateStatisticsAsync(IStatistics statistics);
        Task<IStatistics> GetStatisticsAsync(Guid MatchId);
        Task<bool> UpdateStatisticsAsync(IStatistics statistics);
        Task<bool> DeleteStatisticsAsync(Guid MatchId);
    }
}
