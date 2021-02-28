using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IMatch : IModelBase
    {
        Guid HomeTeamSeasonID { get; set; }
        Guid AwayTeamSeasonID { get; set; }
        Guid LeagueSeasonID { get; set; }
        Guid RefereeID { get; set; }
        string HomeTeam { get; set; }
        string AwayTeam { get; set; }
        string RefereeName { get; set; }
        int MatchDay { get; set; }
        DateTime MatchDate { get; set; } 
        bool IsPlayed { get; set; }
    }
}
