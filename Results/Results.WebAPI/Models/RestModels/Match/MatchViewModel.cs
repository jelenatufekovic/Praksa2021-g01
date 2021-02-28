using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Match
{
    public class MatchViewModel
    {
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string RefereeName { get; set; }
        public int MatchDay { get; set; }
        public DateTime MatchDate { get; set; }
        public bool IsPlayed { get; set; }
    }
}