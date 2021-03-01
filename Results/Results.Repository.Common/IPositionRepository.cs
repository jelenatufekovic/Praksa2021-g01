using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using System;
using System.Threading.Tasks;
using Results.Common.Utils;

namespace Results.Repository.Common
{
    public interface IPositionRepository
    {
        Task<Guid> CreatePositionAsync(IPosition position);
        Task<PagedList<IPosition>> GetPositionAsync(PositionParameters parameters);
        Task<IPosition> GetPositionByIdAsync(Guid id);
        Task<bool> UpdatePositionAsync(IPosition position);
        Task<bool> DeletePositionAsync(Guid id);
    }
}
