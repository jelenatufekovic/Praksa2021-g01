using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;

namespace Results.Service.Common
{
    public interface ICardService
    {
        Task<bool> CreateCardAsync(ICard score);
        Task<bool> UpdateCardAsync(ICard score);
        Task<bool> DeleteCardAsync(Guid id, Guid byUser);
        Task<PagedList<ICard>> GetCardsByQueryAsync(CardParameters parameters);
    }
}
