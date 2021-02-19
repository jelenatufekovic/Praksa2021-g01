using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public class FilterHelper<T, K> : IFilterHelper<T, K>
    {
        public string ApplyFilters(K filterQueryParams, string dateParam)
        {
            var propertyInfos = typeof(K).GetProperties();

            var filterQueryBuilder = new StringBuilder();

            foreach (var property in propertyInfos)
            {
                if (property.Name.Equals("PageNumber") || property.Name.Equals("PageSize") || property.Name.Equals("OrderBy")) { continue; }

                var propertyValue = property.GetValue(filterQueryParams);

                if (String.IsNullOrEmpty(propertyValue?.ToString())) { continue; }

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

                filterQueryBuilder.Append($"{property.Name} = '{propertyValue.ToString()}' AND ");

            }

            string filterQuery = filterQueryBuilder.ToString().TrimEnd(' ', 'A', 'N', 'D', ' ');

            if (String.IsNullOrEmpty(filterQuery))
            {
                return String.Empty;
            }

            return String.Format($" WHERE {filterQuery} ");
        }
    }
}
