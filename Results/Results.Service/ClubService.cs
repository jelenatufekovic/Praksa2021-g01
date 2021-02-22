using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Service.Common;
using Results.Model.Common;
using Results.Repository.Common;

namespace Results.Service
{
    public class ClubService : IClubService
    {
        private readonly IClubRepository _clubRepository;

        public ClubService(IClubRepository clubRepository)
        {
            _clubRepository = clubRepository;
        }
        public async Task<bool> CreateClubAsync(IClub club)
        {
            return await _clubRepository.CreateClubAsync(club);
        }
        public async Task<bool> UpdateClubAsync(IClub club)
        {
            return await _clubRepository.UpdateClubAsync(club);
        }
        public async Task<bool> DeleteClubAsync(IClub club)
        {
            return await _clubRepository.DeleteClubAsync(club);
        }
        public async Task<List<IClub>> GetAllClubsAsync()
        {
            return await _clubRepository.GetAllClubsAsync();
        }
        public async Task<IClub> GetClubByIdAsync(Guid id)
        {
            return await _clubRepository.GetClubByIdAsync(id);
        }
        public async Task<IClub> GetClubByNameAsync(string name)
        {
            return await _clubRepository.GetClubByNameAsync(name);
        }

        public async Task<IClub> GetClubByStadiumIDAsync(Guid StadiumID)
        {
            return await _clubRepository.GetClubByStadiumIDAsync(StadiumID);
        }
    }
}
