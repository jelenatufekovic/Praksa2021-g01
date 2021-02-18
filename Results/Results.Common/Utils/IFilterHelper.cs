using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public interface IFilterHelper<T, K>
    {
        string ApplyFilters(T filterQueryParams, string dateParam);
    }
}
