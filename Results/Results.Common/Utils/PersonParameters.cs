using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public class PersonParameters : QueryParameters
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Country { get; set; }
        
    }
}
