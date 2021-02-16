using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.ViewModels
{
    public class LeagueSeasonViewModel
    {
        public Guid Id { get; set; }
        public Guid LeagueID { get; set; }
        public Guid SeasonID { get; set; }
    }
}