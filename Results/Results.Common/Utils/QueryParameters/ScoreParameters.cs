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
        public Guid Id { get; set; }
        public Guid MatchID { get; set; }
        public Guid PlayerID { get; set; }
        public string MatchMinute { get; set; }
        public bool Autogoal { get; set; }
    }
}
