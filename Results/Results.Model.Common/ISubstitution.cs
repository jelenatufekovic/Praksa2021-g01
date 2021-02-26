using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface ISubstitution : IModelBase
    {
        Guid MatchID { get; set; }
        Guid PlayerInID { get; set; }
        Guid PlayerOutID { get; set; }
        int MatchMinute { get; set; }
    }
}
