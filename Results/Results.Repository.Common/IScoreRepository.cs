using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Repository.Common
{
    public interface IScoreRepository
    {
        Task<bool> CreateScoreAsync(IScore score);
        Task<bool> UpdateScoreAsync(IScore score);
        Task<bool> DeleteScoreAsync(IScore score);
    }
}
