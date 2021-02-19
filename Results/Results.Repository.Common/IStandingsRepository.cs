using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IStandingsRepository
    {
        Task<List<IStandings>> GetTableByLeagueSeasonAsync(Guid guid);
        Task<string> CheckExistingClubForLeagueSeasonAsync(IStandings standings);
        Task<bool> UpdateTableFromDelete(IStandings standings);
        Task<bool> CreateTableByLeagueSeasonAsync(IStandings standings);
        Task<bool> UpdateTableForClubAsync(IStandings standings);
        Task<bool> DeleteTableByLeagueSeasonAsync(IStandings standings);
        Task<bool> DeleteClubTableByLeagueSeasonAsync(IStandings standings);
    }
}
