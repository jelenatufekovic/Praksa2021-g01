using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryHelpers
{
    public interface IRepositoryBase
    {
        IQueryHelper<T, K> GetQueryHelper<T, K>();
        Task<int> GetTableCount<T>();
    }
}
