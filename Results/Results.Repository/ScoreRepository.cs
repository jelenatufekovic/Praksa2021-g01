using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Repository.Common;
using Results.Model.Common;

namespace Results.Repository
{
    public class ScoreRepository : IScoreRepository
    {
        public async Task<bool> CreateScoreAsync(IScore score)
        {
            return true;
        }
        public async Task<bool> UpdateScoreAsync(IScore score)
        {
            return true;
        }
        public async Task<bool> DeleteScoreAsync(IScore score)
        {
            return true;
        }
    }
}
