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
    public class SeasonService : ISeasonService
    {
        protected ISeasonRepository _seasonRepository;

        public SeasonService(ISeasonRepository seasonRepository)
        {
            _seasonRepository = seasonRepository;
        }

        public async Task<bool> CreateSeasonAsync(ISeason season)
        {
            return await _seasonRepository.CreateSeasonAsync(season);
        }

        public async Task<bool> DeleteSeasonAsync(Guid Id)
        {
            return await _seasonRepository.DeleteSeasonAsync(Id);
        }

        public async Task<ISeason> GetSeasonByIdAsync(Guid Id)
        {
            return await _seasonRepository.GetSeasonByIdAsync(Id);
        }

        public async Task<ISeason> GetSeasonByQueryAsync(SeasonParameters parameters)
        {
            return await _seasonRepository.GetSeasonByQueryAsync(parameters);
        }

        public async Task<bool> UpdateSeasonAsync(ISeason season)
        {
            return await _seasonRepository.UpdateSeasonAsync(season);
        }
    }
}