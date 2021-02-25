using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class LeagueSeason : ModelBase, ILeagueSeason
    {
        public Guid LeagueID { get; set; }
        public Guid SeasonID { get; set; }
        public string Category { get; set; }
    }
}
