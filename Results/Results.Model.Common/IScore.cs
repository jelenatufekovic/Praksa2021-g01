using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IScore : IModelBase
    {
        Guid MatchID { get; set; }
        Guid PlayerID { get; set; }
        int MatchMinute { get; set; }
        bool Autogoal { get; set; }
    }
}
