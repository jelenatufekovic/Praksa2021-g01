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
        Guid MatchID { get; set; }
        Guid PlayerInID { get; set; }
        Guid PlayerOutID { get; set; }
        string MatchMinute { get; set; }
    }
}
