using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IStatisticsRepository
    {
        Task<bool> CreateStatisticsAsync(IStatistics statistics);
        Task<IStatistics> GetStatisticsAsync(Guid MatchId);
        Task<bool> UpdateStatisticsAsync(IStatistics statistics);
        Task<bool> DeleteStatisticsAsync(Guid Id);
        Task<List<Guid>> GetClubIDsAsync(Guid MatchId);
    }
}
