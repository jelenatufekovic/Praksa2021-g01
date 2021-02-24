using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface ISeasonService
    {
        Task<bool> CreateSeasonAsync(ISeason season);
        Task<ISeason> GetSeasonByIdAsync(Guid Id);
        Task<ISeason> GetSeasonByQueryAsync(SeasonParameters parameters);
        Task<bool> UpdateSeasonAsync(ISeason season);
        Task<bool> DeleteSeasonAsync(Guid Id);
    }
}