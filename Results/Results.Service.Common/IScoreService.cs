using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Service.Common
{
    public interface IScoreService
    {
        Task<bool> CreateScoreAsync(IScore score);
        Task<bool> UpdateScoreAsync(IScore score);
        Task<bool> DeleteScoreAsync(Guid id, Guid byUser);
    }
}
