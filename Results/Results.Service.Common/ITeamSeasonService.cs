using System;
using System.Collections.Generic;
using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using System.Threading.Tasks;
using Results.Common.Utils;

namespace Results.Service.Common
{
    public interface ITeamSeasonService
    {
        Task<Guid> CreateTeamSeasonAsync(ITeamSeason teamSeason);
        Task<bool> RegisterTeamAsync(List<ITeamRegistration> toRegister);
        Task<List<ITeamSeason>> GetTeamSeasonByQueryAsync(Guid clubID);
        Task<List<ITeamRegistration>> GetTeamByIdAsync(Guid teamSeasonID);
        Task<bool> UpdateTeamAsync(List<Guid> toDelete, List<ITeamRegistration> toRegister);
        Task<bool> DeleteTeamAsync(Guid teamSeasonID);
    }
}
