using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using System;
using System.Threading.Tasks;
using Results.Common.Utils;

namespace Results.Service.Common
{
    public interface IPositionService
    {
        Task<Guid> CreatePositionAsync(IPosition position);
        Task<IPosition> GetPositionByQueryAsync(PositionParameters parameters);
        Task<PagedList<IPosition>> GetPositionsAsync(PositionParameters parameters);
        Task<IPosition> GetPositionByIdAsync(Guid id);
        Task<bool> UpdatePositionAsync(IPosition position);
        Task<bool> DeletePositionAsync(Guid id);
    }
}
