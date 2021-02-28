using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class MatchQueryParameters : QueryParameters
    {
        public Guid? HomeTeamSeasonID { get; set; }
        public Guid? AwayTeamSeasonID { get; set; }
        public Guid? LeagueSeasonID { get; set; }
        public Guid? RefereeID { get; set; }
        public int? MatchDay { get; set; }
        public DateTime? MatchDate { get; set; }
        public bool? IsPlayed { get; set; }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
