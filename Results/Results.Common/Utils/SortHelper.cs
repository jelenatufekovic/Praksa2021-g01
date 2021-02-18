using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public class SortHelper<T> : ISortHelper<T>
    {
        public string ApplySort(string orderByQueryString)
        {
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            var orderParams = orderByQueryString.Trim().Split(',');
            var orderQueryBuilder = new StringBuilder();

            foreach (var param in orderParams)
            {
                if (String.IsNullOrWhiteSpace(param)) { continue; }

                var propertyFromQueryName = param.Split(' ')[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi =>
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null) { continue; }

                var sortingOrder = param.EndsWith(" desc") ? "DESC" : "ASC";

                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");

            }

            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (String.IsNullOrWhiteSpace(orderQuery))
            {
                return String.Empty;
            }
            orderQuery = String.Format($"ORDER BY {0}", orderQuery);

            return orderQuery;
        }
    }
}
