using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IStandings : IModelBase
    {
        Guid LeagueSeasonID { get; set; }
        Guid ClubID { get; set; }
        string ClubName { get; set; }
        int Played { get; set; }
        int Won { get; set; }
        int Draw { get; set; }
        int Lost { get; set; }
        int GoalsScored { get; set; }
        int GoalsConceded { get; set; }
        int Points { get; set; }
    }
}
