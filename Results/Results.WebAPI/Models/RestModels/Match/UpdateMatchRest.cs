using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Match
{
    public class UpdateMatchRest
    {
        public Guid Id { get; set; }
        public Guid RefereeID { get; set; }
        public int MatchDay { get; set; }
        public DateTime MatchDate { get; set; }
        public bool IsPlayed { get; set; }
        public Guid ByUser { get; set; }
    }
}