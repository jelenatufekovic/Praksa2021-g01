using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface ICard : IModelBase
    {
        Guid MatchID { get; set; }
        Guid PlayerID { get; set; }
        bool YellowCard { get; set; }
        bool RedCard { get; set; }
        int MatchMinute { get; set; }
    }
}
