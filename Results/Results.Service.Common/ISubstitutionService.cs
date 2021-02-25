using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Service.Common
{
    public interface ISubstitutionService
    {
        Task<bool> CreateSubstitutionAsync(ISubstitution substitution);
        Task<bool> UpdateSubstitutionAsync(ISubstitution substitution);
        Task<bool> DeleteSubstitutionAsync(Guid id, Guid byUser);
    }
}
