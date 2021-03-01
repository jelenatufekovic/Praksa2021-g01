using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Results.Service.Common;
using Results.Model.Common;
using Results.Repository.Common;
using Results.Repository;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils;

namespace Results.Service
{
    public class PositionService : IPositionService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public PositionService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<Guid> CreatePositionAsync(IPosition position) {
            IPositionRepository positionRepository = _repositoryFactory.GetRepository<PositionRepository>();

            return await positionRepository.CreatePositionAsync(position);
        }

        public async Task<IPosition> GetPositionByQueryAsync(PositionParameters parameters)
        {
            IPositionRepository positionRepository = _repositoryFactory.GetRepository<PositionRepository>();
            PagedList<IPosition> positions = await positionRepository.GetPositionAsync(parameters);
            return positions.Count == 0 ? null : positions[0];
        }

        public async Task<PagedList<IPosition>> GetPositionsAsync(PositionParameters parameters) {
            IPositionRepository positionRepository = _repositoryFactory.GetRepository<PositionRepository>();

            return await positionRepository.GetPositionAsync(parameters);
        }

        public async Task<IPosition> GetPositionByIdAsync(Guid id) {
            IPositionRepository positionRepository = _repositoryFactory.GetRepository<PositionRepository>();
            return await positionRepository.GetPositionByIdAsync(id);
        }

        public async Task<bool> UpdatePositionAsync(IPosition position) {
            IPositionRepository positionRepository = _repositoryFactory.GetRepository<PositionRepository>();
            return await positionRepository.UpdatePositionAsync(position);
        }

        public async Task<bool> DeletePositionAsync(Guid id)
        {
            IPositionRepository positionRepository = _repositoryFactory.GetRepository<PositionRepository>();
            return await positionRepository.DeletePositionAsync(id);
        }
    }
}
