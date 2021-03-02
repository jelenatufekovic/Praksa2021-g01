using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.MatchManager
{
    public class GetAllCardsRest
    {
        public Guid PlayerID { get; set; }
        public bool YellowCard { get; set; }
        public bool RedCard { get; set; }
        public int MatchMinute { get; set; }
    }
}