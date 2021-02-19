using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryHelpers
{
    public class PagingHelper : IPagingHelper
    {
        public string ApplayPaging(int pageNumber, int pageSize)
        {
            var orderQueryBuilder = new StringBuilder();

            orderQueryBuilder.Append($"OFFSET {(pageNumber - 1) * pageSize} ROWS ");
            orderQueryBuilder.Append($"FETCH NEXT {pageSize} ROWS ONLY;");

            return orderQueryBuilder.ToString();
        }
    }
}
