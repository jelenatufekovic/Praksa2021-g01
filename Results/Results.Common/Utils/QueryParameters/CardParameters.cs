﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class CardParameters : QueryParameters
    {
        public CardParameters()
        {
            OrderBy = "MatchMinute";
        }
        Guid MatchID { get; set; }
        Guid PlayerID { get; set; }
        bool YellowCard { get; set; }
        bool RedCard { get; set; }
        string MatchMinute { get; set; }
    }
}
