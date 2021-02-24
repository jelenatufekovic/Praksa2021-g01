using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.League
{
    public class CreateLeagueRest
    {
        public Guid ByUser { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }
        public int Rank { get; set; }
        public string Country { get; set; }
    }
}