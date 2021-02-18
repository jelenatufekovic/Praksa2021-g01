﻿using Results.Common.Utils;
using Results.Model.Common;
using System;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IPlayerRepository
    {
        Task<bool> CreatePlayerAsync(IPlayer player);
        Task<bool> DeletePlayerAsync(Guid id, Guid userId);
        Task<IPlayer> GetPlayerByIdAsync(Guid id);
        //Task<PagedList<IPlayer>> GetPlayersByQuery(PlayerParameters parameters);
        Task<bool> UpdatePlayerAsync(IPlayer player);
    }
}
