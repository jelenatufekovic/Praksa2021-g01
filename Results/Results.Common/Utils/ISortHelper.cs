﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public interface ISortHelper<T>
    {
        string ApplySort(string orderByQueryString);
    }
}
