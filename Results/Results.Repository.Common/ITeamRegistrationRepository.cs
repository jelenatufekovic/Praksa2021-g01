using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils;

namespace Results.Repository.Common
{
    public interface ITeamRegistrationRepository
    {
        Task<bool> CreateTeamRegistrationAsync(ITeamRegistration teamRegistration);
        Task<List<ITeamRegistration>> GetTeamRegistrationsAsync(Guid teamSeasonID, bool includeInactive = false);
        Task<bool> DeactivateTeamRegistrationAsync(Guid id);
        Task<bool> DeleteTeamRegistrationAsync(Guid id);
    }
}
