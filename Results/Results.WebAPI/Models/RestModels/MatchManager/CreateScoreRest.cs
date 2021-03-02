using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.MatchManager
{
    public class CreateScoreRest
    {
        public Guid MatchID { get; set; }
        public Guid PlayerID { get; set; }
        public int MatchMinute { get; set; }
        public bool Autogoal { get; set; }
        public Guid ByUser { get; set; }
    }
}