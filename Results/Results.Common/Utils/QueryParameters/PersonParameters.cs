using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class PersonParameters : QueryParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        public DateTime? MinDateOfBirth { get; set; }
        public DateTime? MaxDateOfBirth { get; set; }

        public virtual new bool IsValid()
        {
            return (base.IsValid() 
                &&
                MinDateOfBirth == null ?
                    true : MinDateOfBirth > DateTime.MinValue && MinDateOfBirth < DateTime.Now
                &&
                MaxDateOfBirth == null ? 
                    true : MaxDateOfBirth <= DateTime.Now && MaxDateOfBirth > DateTime.MinValue
                &&
                MinDateOfBirth < MaxDateOfBirth ? true : false );
        }

    }
}
