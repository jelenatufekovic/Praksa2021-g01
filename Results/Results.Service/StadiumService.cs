using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Service.Common;
using Results.Model.Common;
using Results.Repository.Common;
using Results.Repository;

namespace Results.Service
{
    public class StadiumService : IStadiumService
    {
        private readonly IStadiumRepository _stadiumRepository;

        public StadiumService(IStadiumRepository stadiumRepository)
        {
            _stadiumRepository = stadiumRepository;
        }
        public async Task<bool> CreateStadiumAsync(IStadium stadium)
        {
            return await _stadiumRepository.CreateStadiumAsync(stadium);
        }
        public async Task<bool> UpdateStadiumAsync(IStadium stadium)
        {
            return await _stadiumRepository.UpdateStadiumAsync(stadium);
        }
        public async Task<bool> DeleteStadiumAsync(IStadium stadium)
        {
            return await _stadiumRepository.DeleteStadiumAsync(stadium);
        }
        public async Task<List<IStadium>> GetAllStadiumsAsync()
        {
            return await _stadiumRepository.GetAllStadiumsAsync();
        }
        public async Task<IStadium> GetStadiumByIdAsync(Guid id)
        {
            return await _stadiumRepository.GetStadiumByIdAsync(id);
        }
        public async Task<IStadium> GetStadiumByNameAsync(string name)
        {
            return await _stadiumRepository.GetStadiumByNameAsync(name);
        }
    }
}
