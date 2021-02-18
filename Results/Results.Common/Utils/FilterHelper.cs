using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public class FilterHelper<T, K> : IFilterHelper<T, K>
    {
        public string ApplyFilters(T filterQueryParams, string dateParam)
        {
            var propertyInfos = typeof(T).GetProperties();

            var filterQueryBuilder = new StringBuilder();

            foreach (var property in propertyInfos)
            {
                if (property.Name.Equals("PageNumber") || property.Name.Equals("PageSize")) { continue; }

                var propertyValue = property.GetValue(filterQueryParams);

                if (string.IsNullOrEmpty((string)propertyValue)) { continue; }

                if (property.Name.ToLower().Contains("min"))
                {
                    continue;
                    //filterQueryBuilder.Append($"{dateParam} >= {propertyValue} AND ");
                }
                if (property.Name.ToLower().Contains("max"))
                {
                    continue;
                    //filterQueryBuilder.Append($"{dateParam} <= {propertyValue} AND ");
                }

                filterQueryBuilder.Append($"{property.Name} == {propertyValue} AND ");

            }

            string filterQuery = filterQueryBuilder.ToString().TrimEnd(' ', 'A', 'N', 'D', ' ');

            if (String.IsNullOrEmpty(filterQuery))
            {
                return String.Empty;
            }

            filterQuery = String.Format($"WHERE {0}", filterQuery);
            return filterQuery;
        }
    }
}
