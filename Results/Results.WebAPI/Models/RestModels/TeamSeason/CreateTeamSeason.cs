using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.TeamSeason
{
    public class CreateTeamSeason
    {
        public Guid Id { get; set; }
        public Guid ClubID { get; set; }
        public Guid CoachID { get; set; }
        public Guid LeagueSeasonID { get; set; }
        public string Category { get; set; }
        public Guid ByUser { get; set; }
    }
}