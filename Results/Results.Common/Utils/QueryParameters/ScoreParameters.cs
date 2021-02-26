using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class ScoreParameters : QueryParameters
    {
        public ScoreParameters()
        {
            OrderBy = "MatchMinute";
        }
        Guid MatchID { get; set; }
        Guid PlayerID { get; set; }
        string MatchMinute { get; set; }
        bool Autogoal { get; set; }
    }
}
