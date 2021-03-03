using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IStatistics: IModelBase
    {
        Guid MatchId { get; set; }
        string HomeClubName { get; set; }
        string AwayClubName { get; set; }
        int HomeGoals { get; set; }
        int AwayGoals { get; set; }
        int HomeYellowCards { get; set; }
        int AwayYellowCards { get; set; }
        int HomeRedCards { get; set; }
        int AwayRedCards { get; set; }
        int HomeShots { get; set; }
        int AwayShots { get; set; }
        int HomeShotsOnTarget { get; set; }
        int AwayShotsOnTarget { get; set; }
        int HomePossession { get; set; }
        int AwayPossession { get; set; }
    }
}
