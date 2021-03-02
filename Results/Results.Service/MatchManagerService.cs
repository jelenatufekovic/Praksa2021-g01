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
    public class MatchManagerService : IMatchManagerService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public MatchManagerService(IRepositoryFactory repositoryFactory)
        {
            _repositoryFactory = repositoryFactory;
        }
        public async Task<bool> CreateCardAsync(ICard card)
        {
            ICardRepository cardRepository = _repositoryFactory.GetRepository<CardRepository>();

            return await cardRepository.CreateCardAsync(card);
        }
        public async Task<bool> UpdateCardAsync(ICard card)
        {
            ICardRepository cardRepository = _repositoryFactory.GetRepository<CardRepository>();

            return await cardRepository.UpdateCardAsync(card);
        }
        public async Task<bool> DeleteCardAsync(Guid id, Guid byUser)
        {
            ICardRepository cardRepository = _repositoryFactory.GetRepository<CardRepository>();

            return await cardRepository.DeleteCardAsync(id, byUser);
        }
        public async Task<PagedList<ICard>> GetCardsByQueryAsync(CardParameters parameters)
        {
            ICardRepository cardRepository = _repositoryFactory.GetRepository<CardRepository>();

            return await cardRepository.GetCardsByQueryAsync(parameters);
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

        public async Task<bool> CreateSubstitutionAsync(ISubstitution substitution)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.CreateSubstitutionAsync(substitution);
        }
        public async Task<bool> UpdateSubstitutionAsync(ISubstitution substitution)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.UpdateSubstitutionAsync(substitution);
        }
        public async Task<bool> DeleteSubstitutionAsync(Guid id, Guid byUser)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.DeleteSubstitutionAsync(id, byUser);
        }
        public async Task<PagedList<ISubstitution>> GetSubstitutionsByQueryAsync(SubstitutionParameters parameters)
        {
            ISubstitutionRepository substitutionRepository = _repositoryFactory.GetRepository<SubstitutionRepository>();

            return await substitutionRepository.GetSubstitutionsByQueryAsync(parameters);
        }
    }
}
