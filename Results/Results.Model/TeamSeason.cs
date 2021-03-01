using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Model
{
    public class TeamSeason : ModelBase, ITeamSeason
    {
        public Guid ClubID { get; set; } 
        public Guid CoachID { get; set; }
        public Guid LeagueSeasonID { get; set; }
        public string Category { get; set; }
    }
}
