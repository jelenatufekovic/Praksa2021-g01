using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Service.Common;
using Results.Model.Common;
using Results.Repository.Common;
using Results.Repository;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;

namespace Results.Service
{
    public class StadiumService : IStadiumService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public StadiumService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<bool> CreateStadiumAsync(IStadium stadium)
        {
            IStadiumRepository stadiumRepository = _repositoryFactory.GetRepository<StadiumRepository>();

            return await stadiumRepository.CreateStadiumAsync(stadium);
        }
        public async Task<bool> UpdateStadiumAsync(IStadium stadium)
        {
            IStadiumRepository stadiumRepository = _repositoryFactory.GetRepository<StadiumRepository>();

            return await stadiumRepository.UpdateStadiumAsync(stadium);
        }
        public async Task<bool> DeleteStadiumAsync(IStadium stadium)
        {
            IStadiumRepository stadiumRepository = _repositoryFactory.GetRepository<StadiumRepository>();

            return await stadiumRepository.DeleteStadiumAsync(stadium);
        }
        public async Task<List<IStadium>> GetAllStadiumsAsync()
        {
            IStadiumRepository stadiumRepository = _repositoryFactory.GetRepository<StadiumRepository>();

            return await stadiumRepository.GetAllStadiumsAsync();
        }
        public async Task<IStadium> GetStadiumByIdAsync(Guid id)
        {
            IStadiumRepository stadiumRepository = _repositoryFactory.GetRepository<StadiumRepository>();

            return await stadiumRepository.GetStadiumByIdAsync(id);
        }
        public async Task<PagedList<IStadium>> GetStadiumsByQueryAsync(StadiumParameters parameters)
        {
            IStadiumRepository stadiumRepository = _repositoryFactory.GetRepository<StadiumRepository>();

            return await stadiumRepository.GetStadiumsByQueryAsync(parameters);
        }

    }
}
