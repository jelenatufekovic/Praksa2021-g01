using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface ILeagueSeason : IModelBase
    {
        Guid LeagueID { get; set; }
        Guid SeasonID { get; set; }
        string Category { get; set; }
    }
}
