using Results.Model.Common;
using System;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface ICoachService
    {
        Task<Guid> CreateCoachAsync(ICoach coach);
        Task<bool> DeleteCoachAsync(Guid id, Guid userId);
        Task<ICoach> GetCoachByIdAsync(Guid id);
        Task<bool> UpdateCoachAsync(ICoach coach);
    }
}
