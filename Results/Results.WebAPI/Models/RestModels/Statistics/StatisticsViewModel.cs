using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Statistics
{
    public class StatisticsViewModel
    {
        public string HomeClubName { get; set; }
        public string AwayClubName { get; set; }
        public int HomeGoals { get; set; }
        public int AwayGoals { get; set; }
        public int HomeYellowCards { get; set; }
        public int AwayYellowCards { get; set; }
        public int HomeRedCards { get; set; }
        public int AwayRedCards { get; set; }
        public int HomeShots { get; set; }
        public int AwayShots { get; set; }
        public int HomeShotsOnTarget { get; set; }
        public int AwayShotsOnTarget { get; set; }
        public int HomePossession { get; set; }
        public int AwayPossession { get; set; }
    }
}