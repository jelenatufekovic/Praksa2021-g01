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
        public DateTime? MinCreatedAt { get; set; }
        public DateTime? MaxCreatedAt { get; set; }
        public DateTime? MinUpdatedAt { get; set; }
        public DateTime? MaxUpdatedAt { get; set; }

        public virtual bool IsValid()
        {
            return (
                MinCreatedAt == null ?
                    true : 
                    MinCreatedAt > DateTime.MinValue && MinCreatedAt < DateTime.Now
                &&
                MaxCreatedAt == null ?
                    true :
                    MaxCreatedAt <= DateTime.Now && MaxCreatedAt > DateTime.MinValue
                && 
                MinCreatedAt == null && MaxCreatedAt == null ?
                    true :
                    MinCreatedAt < MaxCreatedAt
                &&
                MinUpdatedAt == null ?
                    true :
                    MinUpdatedAt > DateTime.MinValue && MinUpdatedAt < DateTime.Now
                &&
                MaxUpdatedAt == null ?
                    true :
                    MaxUpdatedAt <= DateTime.Now && MaxUpdatedAt > DateTime.MinValue
                &&
                MinUpdatedAt == null && MaxUpdatedAt == null ?
                    true :
                    MinCreatedAt < MaxUpdatedAt);
        }

    }
}
