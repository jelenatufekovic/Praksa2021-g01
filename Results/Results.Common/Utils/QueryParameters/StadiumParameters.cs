using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class StadiumParameters : QueryParameters
    {
        public StadiumParameters()
        {
            OrderBy = "Name";
        }

        public string Name { get; set; }
        public string StadiumAddres { get; set; }
        public string Capacity { get; set; }
        public DateTime YearOfConstruction { get; set; }
            
    }
}
