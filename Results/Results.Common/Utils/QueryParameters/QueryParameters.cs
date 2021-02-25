using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;


namespace Results.Common.Utils.QueryParameters
{
    public abstract class QueryParameters
    {
        const int maxPageSize = 100;
        private int _pageSize = 10;

        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
        
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

        public string IsDeleted { get; set; }
        public string ByUser { get; set; }
        public DateTime MinCreatedAt { get; set; }
        public string MaxCreatedAt { get; set; } = DateTime.Now.ToString("dd'-'MMM'-'yy HH:mm:ss", CultureInfo.InvariantCulture);
        public DateTime MinUpdatedAt { get; set; }
        public string MaxUpdatedAt { get; set; } = DateTime.Now.ToString("dd'-'MMM'-'yy HH:mm:ss", CultureInfo.InvariantCulture);

        public virtual bool IsValid()
        {
            return (MinCreatedAt < DateTime.Parse(MaxCreatedAt) && MinUpdatedAt < DateTime.Parse(MaxUpdatedAt));
        }

    }
}
