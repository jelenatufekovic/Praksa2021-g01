using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;

namespace Results.Repository.Common
{
    public interface IClubRepository
    {
        Task<bool> CreateClubAsync(IClub club);
        Task<bool> UpdateClubAsync(IClub club);
        Task<bool> DeleteClubAsync(IClub club);
        Task<List<IClub>> GetAllClubsAsync();
        Task<IClub> GetClubByIdAsync(Guid id);
        Task<PagedList<IClub>> GetClubsByQueryAsync(ClubParameters parameters);
    }
}
