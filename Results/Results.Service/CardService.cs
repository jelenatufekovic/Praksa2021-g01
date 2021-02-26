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
    public class CardService : ICardService
    {
        private readonly IRepositoryFactory _repositoryFactory;

        public CardService(IRepositoryFactory repositoryFactory)
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
    }
}
