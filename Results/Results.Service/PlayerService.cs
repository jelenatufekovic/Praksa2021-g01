using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IPersonService _personService;

        public PlayerService(IPlayerRepository playerRepository, IPersonService personService)
        {
            _playerRepository = playerRepository;
            _personService = personService;
        }

        public async Task<Guid> CreatePlayerAsync(IPlayer player)
        {
            player.Id = await _personService.CreatePersonAsync(player);
            await _playerRepository.CreatePlayerAsync(player);
            return player.Id;
        }

        public async Task<bool> DeletePlayerAsync(Guid id, Guid userId) => await _playerRepository.DeletePlayerAsync(id, userId);

        public async Task<IPlayer> GetPlayerByIdAsync(Guid id) => await _playerRepository.GetPlayerByIdAsync(id);

        public async Task<bool> UpdatePlayerAsync(IPlayer player)
        {
            if (!(await _personService.UpdatePersonAsync(player)))
            {
                return false;
            }
            return await _playerRepository.UpdatePlayerAsync(player);
        }
    }
}
