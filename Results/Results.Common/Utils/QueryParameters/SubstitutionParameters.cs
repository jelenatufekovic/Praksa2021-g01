using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class SubstitutionParameters : QueryParameters
    {
        public SubstitutionParameters()
        {
            OrderBy = "MatchMinute";
        }
        public Guid Id { get; set; }
        public Guid MatchID { get; set; }
        public Guid PlayerInID { get; set; }
        public Guid PlayerOutID { get; set; }
        public string MatchMinute { get; set; }
    }
}
