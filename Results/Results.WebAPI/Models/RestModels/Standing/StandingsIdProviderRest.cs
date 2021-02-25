using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Standing
{
    public class StandingsIdProviderRest
    {
        public Guid LeagueSeasonID { get; set; }
        public Guid ClubID { get; set; }
        public Guid ByUser { get; set; }
    }
}