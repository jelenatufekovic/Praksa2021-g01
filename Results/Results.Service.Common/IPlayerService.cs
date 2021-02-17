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
        Task<bool> UpdatePlayerAsync(IPlayer player);
    }
}
