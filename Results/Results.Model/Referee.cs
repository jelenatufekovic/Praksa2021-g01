﻿using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class Referee : Person, IReferee
    {
        public int Rating { get; set; }
    }
}
