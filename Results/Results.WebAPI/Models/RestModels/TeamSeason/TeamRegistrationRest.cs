using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.TeamSeason
{
    public class TeamRegistrationRest
    {
        public Guid Id { get; set; }
        public Guid PlayerId { get; set; }
        public Guid PositionId { get; set; }
        public int JerseyNumber { get; set; }
        public Guid ByUser { get; set; }

        public bool IsValid() {
            return JerseyNumber > 0;
        }
    }
}