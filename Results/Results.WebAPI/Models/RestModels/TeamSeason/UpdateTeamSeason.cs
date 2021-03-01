using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.Models.RestModels.TeamSeason
{
    public class UpdateTeamSeason
    {
        public List<Guid> ToDelete { get; set; }
        public List<TeamRegistrationRest> ToRegister { get; set; }
    }
}