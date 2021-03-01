using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils;

namespace Results.Repository.Common
{
    public interface ITeamSeasonRepository : IRepositoryBase
    {
        Task<Guid> CreateTeamSeasonAsync(ITeamSeason teamSeason);
        Task<List<ITeamSeason>> GetTeamSeasonAsync(Guid clubID);
        Task<bool> DeleteTeamSeasonAsync(Guid id);
    }
}
