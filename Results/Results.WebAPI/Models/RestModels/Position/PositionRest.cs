using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Position
{
    public class PositionRest
    {
        public string Name { get; set; }

        public string ShortName { get; set; }

        public Guid ByUser { get; set; }

    }
}