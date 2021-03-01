using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface ITeamRegistration : IModelBase
    {
        Guid TeamSeasonId { get; set; }
        Guid PlayerId { get; set; }
        Guid PositionId { get; set; }
        int JerseyNumber { get; set; }
        bool IsActive { get; set; }
    }
}
