using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Model
{
    public class Substitution : ModelBase, ISubstitution
    {
        public Guid MatchID { get; set; }
        public Guid PlayerInID { get; set; }
        public Guid PlayerOutID { get; set; }
        public int MatchMinute { get; set; }
    }
}
