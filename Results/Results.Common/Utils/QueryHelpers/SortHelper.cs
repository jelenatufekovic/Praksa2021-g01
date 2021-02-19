using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Results.Common.Utils.QueryHelpers
{
    public class SortHelper<T> : ISortHelper<T>
    {
        public string ApplySort(string orderByQueryString)
        {
            List<PropertyInfo> propertyInfos = typeof(T).GetInterfaces().SelectMany(i => i.GetProperties()).ToList();
            List<PropertyInfo> interfaceProperties = typeof(T).GetProperties().ToList();
            propertyInfos.AddRange(interfaceProperties);

            StringBuilder orderQueryBuilder = new StringBuilder();
            String[] orderParams = orderByQueryString.Trim().Split(',');

            foreach (var param in orderParams)
            {
                if (String.IsNullOrWhiteSpace(param)) { continue; }

                string propertyNameFromQuery = param.Split(' ')[0];
                PropertyInfo objectProperty = propertyInfos.FirstOrDefault(p => 
                        p.Name.Equals(propertyNameFromQuery, StringComparison.InvariantCultureIgnoreCase));

                if (objectProperty == null) { continue; }

                string sortingOrder = param.EndsWith(" desc") ? "DESC" : "ASC";

                orderQueryBuilder.Append($"{objectProperty.Name} {sortingOrder}, ");
            }

            string orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');

            if (String.IsNullOrWhiteSpace(orderQuery))
            {
                return String.Empty;
            }

            return String.Format("ORDER BY {0} ", orderQuery);
        }
    }
}
