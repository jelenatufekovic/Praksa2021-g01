using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IPersonRepository
    {
        Task<Guid> CreatePersonAsync(IPerson person);
        Task<bool> UpdatePersonAsync(IPerson person);
    }
}
