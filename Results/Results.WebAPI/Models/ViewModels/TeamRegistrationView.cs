using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.ViewModels
{
    public class TeamRegistrationView
    {
        public Guid Id { get; set; }
        public Guid TeamSeasonId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid PositionId { get; set; }
        public int JerseyNumber { get; set; }
    }
}