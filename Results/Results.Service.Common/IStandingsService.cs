using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface IStandingsService
    {
        Task<List<IStandingsModel>> GetTableByLeagueSeasonAsync(Guid guid);
        Task<string> CheckExistingClubForLeagueSeasonAsync(IStandingsModel standings);
        Task<bool> UpdateTableFromDelete(IStandingsModel standings);
        Task<bool> CreateTableByLeagueSeasonAsync(IStandingsModel standings);
        Task<bool> UpdateTableForClubAsync(IStandingsModel standings);
        Task<bool> DeleteTableByLeagueSeasonAsync(IStandingsModel standings);
        Task<bool> DeleteClubTableByLeagueSeasonAsync(IStandingsModel standings);

    }
}
