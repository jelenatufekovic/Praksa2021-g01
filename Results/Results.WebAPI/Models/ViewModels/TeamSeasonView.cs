using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.ViewModels
{
    public class TeamSeasonView
    {
        public Guid ClubID { get; set; }
        public Guid CoachID { get; set; }
        public Guid LeagueSeasonID { get; set; }
        public string Category { get; set; }
    }
}