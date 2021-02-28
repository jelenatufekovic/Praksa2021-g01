using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Match
{
    public class MatchIdProviderRest
    {
        public Guid HomeTeamSeasonID { get; set; }
        public Guid AwayTeamSeasonID { get; set; }
        public Guid LeagueSeasonID { get; set; }
        public Guid RefereeID { get; set; }
    }
}