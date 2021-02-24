using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.Club
{
    public class UpdateClubRest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid StadionID { get; set; }
        public string ClubAddress { get; set; }
        public string ShortName { get; set; }
        public string Description { get; set; }
        public Guid ByUser { get; set; }
    }
}