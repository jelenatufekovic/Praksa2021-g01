using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service.Common
{
    public interface IPersonService
    {
        Task<Guid> CreatePersonAsync(IPerson person);
        Task<bool> UpdatePersonAsync(IPerson person);
    }
}
