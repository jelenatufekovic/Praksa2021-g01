﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class PlayerParameters : PersonParameters
    {
        const int minPlayerValue = 1000;
        public PlayerParameters()
        {
            OrderBy = "LastName";
        }

        public int MinPlayerValue { get; set; } = -1;
        public int MaxPlayerValue { get; set; } = -1;

        public override bool IsValid()
        {
            return (base.IsValid() &&
                MinPlayerValue != -1 ?
                    MinPlayerValue > 0 ?
                        MinPlayerValue >= minPlayerValue
                        : false
                    : true &&
                MaxPlayerValue != -1 ?
                    MaxPlayerValue > 0 ?
                        MaxPlayerValue < Int32.MaxValue
                        : false
                    : true &&
                MaxPlayerValue != -1 && MinPlayerValue != -1 ?
                    MaxPlayerValue > 0 && MinPlayerValue > 0 ?
                        MaxPlayerValue >= MinPlayerValue
                        : false
                    : true);
        }
    }
}
