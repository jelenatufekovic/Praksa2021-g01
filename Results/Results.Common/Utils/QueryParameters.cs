using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public abstract class QueryParameters
    {
        const int maxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        public bool IsDeleted { get; set; }
        public string ByUser { get; set; }
        public string OrderBy { get; set; }

        private int _pageSize = 10;
        public int PageSize
        {
            get
            {
                return _pageSize;
            }

            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }
        
    }
}
