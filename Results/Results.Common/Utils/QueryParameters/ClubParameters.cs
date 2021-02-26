using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class ClubParameters : QueryParameters
    {
        public ClubParameters()
        {
            OrderBy = "Name";
        }

        public Guid StadiumID { get; set; }
        public string Name { get; set; }
        public string ClubAddress { get; set; }
        public string ShortName { get; set; }
        public DateTime? YearOfFoundation { get; set; }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
