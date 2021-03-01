using System;
using System.Collections.Generic;
using Results.Model.Common;

namespace Results.Model
{
    public class TeamRegistration : ModelBase, ITeamRegistration
    {
        public Guid TeamSeasonId { get; set; }
        public Guid PlayerId { get; set; }
        public Guid PositionId { get; set; }
        public int JerseyNumber { get; set; }
        public bool IsActive { get; set; }
    }
}
