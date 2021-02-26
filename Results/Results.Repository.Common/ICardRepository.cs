using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;

namespace Results.Repository.Common
{
    public interface ICardRepository
    {
        Task<bool> CreateCardAsync(ICard card);
        Task<bool> UpdateCardAsync(ICard card);
        Task<bool> DeleteCardAsync(Guid id, Guid byUser);
        Task<PagedList<ICard>> GetCardsByQueryAsync(CardParameters parameters);
    }
}
