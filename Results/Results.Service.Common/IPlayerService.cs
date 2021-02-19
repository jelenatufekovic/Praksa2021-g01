using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface IPlayerService
    {
        Task<Guid> CreatePlayerAsync(IPlayer player);
        Task<bool> DeletePlayerAsync(Guid id, Guid userId);
        Task<IPlayer> GetPlayerByIdAsync(Guid id);
        Task<PagedList<IPlayer>> GetPlayersByQueryAsync(PlayerParameters parameters);
        Task<bool> UpdatePlayerAsync(IPlayer player);
    }
}
