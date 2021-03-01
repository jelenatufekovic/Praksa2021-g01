using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface ITeamSeason : IModelBase
    {
        Guid ClubID { get; set; }
        Guid CoachID { get; set; }
        Guid LeagueSeasonID { get; set; }
        string Category { get; set; }

    }
}
