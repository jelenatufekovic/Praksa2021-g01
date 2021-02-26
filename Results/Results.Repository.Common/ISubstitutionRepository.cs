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
    public interface ISubstitutionRepository
    {
        Task<bool> CreateSubstitutionAsync(ISubstitution substitution);
        Task<bool> UpdateSubstitutionAsync(ISubstitution substitution);
        Task<bool> DeleteSubstitutionAsync(Guid id, Guid byUser);
        Task<PagedList<ISubstitution>> GetSubstitutionsByQueryAsync(SubstitutionParameters parameters);
    }
}
