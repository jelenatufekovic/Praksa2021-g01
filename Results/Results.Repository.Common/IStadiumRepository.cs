using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Model.Common;
using Results.Common.Utils.QueryParameters;

namespace Results.Repository.Common
{
    public interface IStadiumRepository
    {
        Task<bool> CreateStadiumAsync(IStadium stadium);
        Task<bool> UpdateStadiumAsync(IStadium stadium);
        Task<bool> DeleteStadiumAsync(IStadium stadium);
        Task<List<IStadium>> GetAllStadiumsAsync();
        Task<IStadium> GetStadiumByIdAsync(Guid id);
        Task<PagedList<IStadium>> GetStadiumsByQueryAsync(StadiumParameters parameters);
    }
}
