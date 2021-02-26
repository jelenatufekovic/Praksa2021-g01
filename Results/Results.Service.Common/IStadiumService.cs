using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;

namespace Results.Service.Common
{
    public interface IStadiumService
    {
        Task<bool> CreateStadiumAsync(IStadium stadium);
        Task<bool> UpdateStadiumAsync(IStadium stadium);
        Task<bool> DeleteStadiumAsync(IStadium stadium);
        Task<List<IStadium>> GetAllStadiumsAsync();
        Task<IStadium> GetStadiumByIdAsync(Guid id);
        Task<PagedList<IStadium>> GetStadiumsByQueryAsync(StadiumParameters parameters);
    }
}
