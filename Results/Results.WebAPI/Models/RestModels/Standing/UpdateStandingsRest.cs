using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Standing
{
    public class UpdateStandingsRest
    {
        public Guid LeagueSeasonID { get; set; }
        public Guid ClubID { get; set; }
        public int Won { get; set; }
        public int Draw { get; set; }
        public int Lost { get; set; }
        public int GoalsScored { get; set; }
        public int GoalsConceded { get; set; }
        public Guid ByUser { get; set; }
    }
}