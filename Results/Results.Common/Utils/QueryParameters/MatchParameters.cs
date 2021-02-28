using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class MatchParameters
    {
        public Guid HomeTeamSeasonID { get; set; }
        public Guid AwayTeamSeasonID { get; set; }
        public Guid LeagueSeasonID { get; set; }
        public int MatchDay { get; set; }
    }
}
