using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Service.Common;
using Results.Model.Common;
using Results.Repository.Common;
using Results.Common.Utils.QueryParameters;
using Results.Repository;
using Results.Common.Utils;

namespace Results.Service
{
    public class ClubService : IClubService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public ClubService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<bool> CreateClubAsync(IClub club)
        {
            IClubRepository clubRepository = _repositoryFactory.GetRepository<ClubRepository>();

            return await clubRepository.CreateClubAsync(club);
        }
        public async Task<bool> UpdateClubAsync(IClub club)
        {
            IClubRepository clubRepository = _repositoryFactory.GetRepository<ClubRepository>();

            return await clubRepository.UpdateClubAsync(club);
        }
        public async Task<bool> DeleteClubAsync(IClub club)
        {
            IClubRepository clubRepository = _repositoryFactory.GetRepository<ClubRepository>();

            return await clubRepository.DeleteClubAsync(club);
        }
        public async Task<List<IClub>> GetAllClubsAsync()
        {
            IClubRepository clubRepository = _repositoryFactory.GetRepository<ClubRepository>();

            return await clubRepository.GetAllClubsAsync();
        }
        public async Task<IClub> GetClubByIdAsync(Guid id)
        {
            IClubRepository clubRepository = _repositoryFactory.GetRepository<ClubRepository>();

            return await clubRepository.GetClubByIdAsync(id);
        }

        public async Task<PagedList<IClub>> GetClubsByQueryAsync(ClubParameters parameters)
        {
            IClubRepository clubRepository = _repositoryFactory.GetRepository<ClubRepository>();

            return await clubRepository.GetClubsByQueryAsync(parameters);
        }
    }
}
