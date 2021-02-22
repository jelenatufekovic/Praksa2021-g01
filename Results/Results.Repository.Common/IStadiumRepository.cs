using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Repository.Common
{
    public interface IStadiumRepository
    {
        Task<bool> CreateStadiumAsync(IStadium stadium);
        Task<bool> UpdateStadiumAsync(IStadium stadium);
        Task<bool> DeleteStadiumAsync(IStadium stadium);
        Task<List<IStadium>> GetAllStadiumsAsync();
        Task<IStadium> GetStadiumByIdAsync(Guid id);
        Task<IStadium> GetStadiumByNameAsync(string name);
    }
}
