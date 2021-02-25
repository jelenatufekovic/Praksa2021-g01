using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryHelpers
{
    public class FilterHelper<T, K> : IFilterHelper<T, K>
    {
        public string ApplyFilters(K filterQueryParams)
        {
            PropertyInfo[] propertyInfos = typeof(K).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            List<PropertyInfo> propertyInfosFromModel = typeof(T).GetInterfaces().SelectMany(i => i.GetProperties()).ToList();
            List<PropertyInfo> interfaceProperties = typeof(T).GetProperties().ToList();
            propertyInfosFromModel.AddRange(interfaceProperties);

            StringBuilder filterQueryBuilder = new StringBuilder();

            foreach (var property in propertyInfos)
            {
                if (property.Name.Equals("PageNumber") || property.Name.Equals("PageSize") || property.Name.Equals("OrderBy")) { continue; }
                
                var propertyValue = property.GetValue(filterQueryParams);

                if (String.IsNullOrEmpty(propertyValue?.ToString())) { continue; }
                
                string propertyName = property.Name;

                if (propertyName.ToLower().Contains("min"))
                {
                    PropertyInfo objectProperty = propertyInfosFromModel.FirstOrDefault(p =>
                        p.Name.Equals(propertyName.Substring(3), StringComparison.InvariantCultureIgnoreCase));

                    if (objectProperty != null)
                    {
                        filterQueryBuilder.Append($"{objectProperty.Name} >= '{propertyValue}' AND ");
                    }
                    continue;
                }

                if (propertyName.ToLower().Contains("max"))
                {
                    PropertyInfo objectProperty = propertyInfosFromModel.FirstOrDefault(p =>
                        p.Name.Equals(propertyName.Substring(3), StringComparison.InvariantCultureIgnoreCase));
                    if (objectProperty != null)
                    {
                        filterQueryBuilder.Append($"{objectProperty.Name} <= '{propertyValue}' AND ");
                    }
                    continue;
                }

                filterQueryBuilder.Append($"{property.Name} = '{propertyValue}' AND ");

            }

            string filterQuery = filterQueryBuilder.ToString().TrimEnd(' ', 'A', 'N', 'D', ' ');

            if (String.IsNullOrEmpty(filterQuery))
            {
                return String.Empty;
            }

            return String.Format("WHERE {0} ", filterQuery);
        }
    }
}
