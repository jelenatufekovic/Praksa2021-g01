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
        Task<List<IStandingsModel>> GetTableByLeagueSeasonAsync(Guid guid);
        Task<bool> CheckExistingClubForLeagueSeasonAsync(IStandingsModel standings);
        Task<bool> CreateTableByLeagueSeasonAsync(IStandingsModel standings);
    }
}
