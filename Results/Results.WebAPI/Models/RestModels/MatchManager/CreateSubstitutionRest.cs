using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.MatchManager
{
    public class CreateSubstitutionRest
    {
        public Guid MatchID { get; set; }
        public Guid PlayerInID { get; set; }
        public Guid PlayerOutID { get; set; }
        public int MatchMinute { get; set; }
        public Guid ByUser { get; set; }
    }
}