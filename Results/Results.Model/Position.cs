using System;
using System.Collections.Generic;
using Results.Model.Common;

namespace Results.Model
{
    public class Position : ModelBase, IPosition
    {
        public string Name { get; set; }

        public string ShortName { get; set; }
    }
}
