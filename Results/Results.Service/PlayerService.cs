﻿using Results.Common.Utils;
using Results.Model.Common;
using Results.Repository;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service
{
    public class PlayerService : IPlayerService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public PlayerService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<Guid> CreatePlayerAsync(IPlayer player)
        {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                player.Id = await unitOfWork.Person.CreatePersonAsync(player);

                await unitOfWork.Player.CreatePlayerAsync(player);
                unitOfWork.Commit();

                return player.Id;
            }
        }

        public async Task<IPlayer> GetPlayerByIdAsync(Guid id)
        {
            IPlayerRepository playerRepository = _repositoryFactory.GetRepository<PlayerRepository>();

            return await playerRepository.GetPlayerByIdAsync(id);
        }
        public async Task<PagedList<IPlayer>> GetPlayersByQueryAsync(PlayerParameters parameters)
        {
            IPlayerRepository playerRepository = _repositoryFactory.GetRepository<PlayerRepository>();

            return await playerRepository.GetPlayersByQueryAsync(parameters);
        }

        public async Task<bool> DeletePlayerAsync(Guid id, Guid userId)
        {
            IPlayerRepository playerRepository = _repositoryFactory.GetRepository<PlayerRepository>();

            return await playerRepository.DeletePlayerAsync(id, userId);
        }


        public async Task<bool> UpdatePlayerAsync(IPlayer player)
        {
            using (IUnitOfWork unitOfWork = _repositoryFactory.GetUnitOfWork())
            {
                await unitOfWork.Person.UpdatePersonAsync(player);
                await unitOfWork.Player.UpdatePlayerAsync(player);
                unitOfWork.Commit();
                
                return true;
            }
        }
    }
}
