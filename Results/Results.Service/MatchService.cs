using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;

        public MatchService(IMatchRepository matchRepository)
        {
            _matchRepository = matchRepository;
        }
        public async Task<Guid> CreateMatchAsync(IMatch match)
        {
            return await _matchRepository.CreateMatchAsync(match);
        }
        public async Task<bool> CheckMatchExistingAsync(MatchParameters parameters)
        {
            return await _matchRepository.CheckMatchExistingAsync(parameters);
        }
        public async Task<bool> DeleteMatchAsync(Guid id, Guid ByUser)
        {
            return await _matchRepository.DeleteMatchAsync(id, ByUser);
        }
        public async Task<bool> UpdateMatchAsync(IMatch match)
        {
            return await _matchRepository.UpdateMatchAsync(match);
        }
        public async Task<IMatch> GetMatchByIdAsync(Guid id)
        {
            return await _matchRepository.GetMatchByIdAsync(id);
        }
        public async Task<PagedList<IMatch>> GetMatchByQueryAsync(MatchQueryParameters parameters)
        {
            return await _matchRepository.GetMatchByQueryAsync(parameters);
        }
    }
}
