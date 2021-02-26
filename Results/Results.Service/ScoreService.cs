using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Repository;
using Results.Repository.Common;
using Results.Service.Common;

namespace Results.Service
{
    public class ScoreService : IScoreService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public ScoreService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }

        public async Task<bool> CreateScoreAsync(IScore score)
        {
            IScoreRepository scoreRepository = _repositoryFactory.GetRepository<ScoreRepository>();

            return await scoreRepository.CreateScoreAsync(score);
        }

        public async Task<bool> UpdateScoreAsync(IScore score)
        {
            IScoreRepository scoreRepository = _repositoryFactory.GetRepository<ScoreRepository>();

            return await scoreRepository.UpdateScoreAsync(score);
        }

        public async Task<bool> DeleteScoreAsync(Guid id, Guid byUser)
        {
            IScoreRepository scoreRepository = _repositoryFactory.GetRepository<ScoreRepository>();

            return await scoreRepository.DeleteScoreAsync(id, byUser);
        }
        public async Task<PagedList<IScore>> GetScoresByQueryAsync(ScoreParameters parameters)
        {
            IScoreRepository scoreRepository = _repositoryFactory.GetRepository<ScoreRepository>();

            return await scoreRepository.GetScoresByQueryAsync(parameters);
        }
    }
}
