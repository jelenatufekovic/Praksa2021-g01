using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface IStandingsService
    {
        Task<List<IStandings>> GetStandingsByLeagueSeasonAsync(Guid id);
        Task<IStandings> GetStandingsByQueryAsync(StandingsParameters parameters);
        Task<bool> CheckStandingsForClubAsync(IStandings standings);
        Task<bool> CreateStandingsForClubAsync(IStandings standings);
        Task<bool> UpdateStandingsForClubAsync(IStandings standings);
        Task<bool> DeleteLeagueSeasonStandingsAsync(IStandings standings);
        Task<bool> DeleteClubStandingsAsync(IStandings standings);

    }
}
