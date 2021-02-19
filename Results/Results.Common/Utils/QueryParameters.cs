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

        public bool IsDeleted { get; set; }
        public string ByUser { get; set; }
        public DateTime MinCreatedAt { get; set; }
        public DateTime MaxCreatedAt { get; set; } = DateTime.Now;
        public DateTime MinUpdatedAt { get; set; }
        public DateTime MaxUpdatedAt { get; set; } = DateTime.Now;

        public virtual bool IsValid()
        {
            return (MinCreatedAt < MaxCreatedAt && MinUpdatedAt < MaxUpdatedAt);
        }

    }
}
