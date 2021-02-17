using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.LeagueSeason
{
    public class CreateLeagueSeasonRest
    {
        public Guid LeagueID { get; set; }
        public Guid SeasonID { get; set; }
        public string Category { get; set; }
        public Guid ByUser { get; set; }
    }
}