using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Club
{
    public class GetAllClubsRest
    {
        public string Name { get; set; }
        public string ClubAddress { get; set; }
        public string ShortName { get; set; }
        public DateTime YearOfFoundation { get; set; }
        public string Description { get; set; }
    }
}