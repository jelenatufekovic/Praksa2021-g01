using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Season
{
    public class UpdateSeasonRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Period { get; set; }
        public int YearOfStart { get; set; }
        public Guid ByUser { get; set; }
    }
}