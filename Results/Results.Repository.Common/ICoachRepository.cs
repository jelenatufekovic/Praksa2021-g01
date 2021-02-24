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
    public interface ICoachRepository
    {
        Task<bool> CreateCoachAsync(ICoach coach);
        Task<bool> DeleteCoachAsync(Guid id, Guid userId);
        Task<ICoach> GetCoachByIdAsync(Guid id);
        Task<PagedList<ICoach>> FindCoachesAsync(CoachParameters parameters);
        Task<bool> UpdateCoachAsync(ICoach coach);
    }
}
