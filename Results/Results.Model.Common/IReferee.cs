﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IReferee
    {
        Guid PersonId { get; set; }
        int Rating { get; set; }
    }
}
