using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Model
{
    public class Score : ModelBase, IScore
    {
        public Guid MatchID { get; set; }
        public Guid PlayerID { get; set; }
        public int MatchMinute { get; set; }
        public bool Autogoal { get; set; }
    }
}
