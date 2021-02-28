using Results.Model.Common;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IMatchRepository
    {
        Task<Guid> CreateMatchAsync(IMatch match);
        Task<bool> CheckMatchExistingAsync(MatchParameters parameters);
        Task<bool> DeleteMatchAsync(Guid id, Guid ByUser);
        Task<bool> UpdateMatchAsync(IMatch match);
        Task<IMatch> GetMatchByIdAsync(Guid id);
        Task<PagedList<IMatch>> GetMatchByQueryAsync(MatchQueryParameters parameters);
    }
}
