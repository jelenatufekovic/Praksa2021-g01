using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Repository.Common;
using Results.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service
{
    class LeagueService : ILeagueService
    {
        protected ILeagueRepository _leagueRepository;

        public LeagueService(ILeagueRepository leagueRepository)
        {
            _leagueRepository = leagueRepository;
        }

        public async Task<bool> CreateLeagueAsync(ILeague league)
        {
            return await _leagueRepository.CreateLeagueAsync(league);
        }

        public async Task<bool> DeleteLeagueAsync(Guid Id)
        {
            return await _leagueRepository.DeleteLeagueAsync(Id);
        }

        public async Task<ILeague> GetLeagueByIdAsync(Guid Id)
        {
            return await _leagueRepository.GetLeagueByIdAsync(Id);
        }

        public async Task<ILeague> GetLeagueByQueryAsync(LeagueParameters parameters)
        {
            return await _leagueRepository.GetLeagueByQueryAsync(parameters);
        }

        public async Task<bool> UpdateLeagueAsync(ILeague league)
        {
            return await _leagueRepository.UpdateLeagueAsync(league);
        }
    }
}