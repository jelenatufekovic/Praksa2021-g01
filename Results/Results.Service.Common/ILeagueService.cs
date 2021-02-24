
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface ILeagueService
    {
        Task<bool> CreateLeagueAsync(ILeague league);
        Task<ILeague> GetLeagueByIdAsync(Guid Id);
        Task<ILeague> GetLeagueByQueryAsync(LeagueParameters parameters);
        Task<bool> UpdateLeagueAsync(ILeague league);
        Task<bool> DeleteLeagueAsync(Guid Id);
    }
}