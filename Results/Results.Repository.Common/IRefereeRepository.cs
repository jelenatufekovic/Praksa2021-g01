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
    public interface IRefereeRepository
    {
        Task<bool> CreateRefereeAsync(IReferee referee);
        Task<bool> DeleteRefereeAsync(Guid id, Guid userId);
        Task<IReferee> GetRefereeByIdAsync(Guid id);
        Task<PagedList<IReferee>> FindRefereesAsync(RefereeParameters parameters);
        Task<bool> UpdateRefereeAsync(IReferee referee);
    }
}
