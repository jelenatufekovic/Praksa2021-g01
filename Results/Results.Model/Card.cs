using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Model
{
    public class Card : ModelBase, ICard
    {
        public Guid MatchID { get; set; }
        public Guid PlayerID { get; set; }
        public bool YellowCard { get; set; }
        public bool RedCard { get; set; }
        public int MatchMinute { get; set; }
    }
}
