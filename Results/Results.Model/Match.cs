using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class Match : ModelBase, IMatch
    {
        public Guid HomeTeamSeasonID { get; set; }
        public Guid AwayTeamSeasonID { get; set; }
        public Guid LeagueSeasonID { get; set; }
        public Guid RefereeID { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string RefereeName { get; set; }
        public int MatchDay { get; set; }
        public DateTime MatchDate { get; set; } 
        public bool IsPlayed { get; set; }
    }
}
