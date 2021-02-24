using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class StatisticsParameters: QueryParameters
    {
        public Guid TeamSeasonID { get; set; } = Guid.Parse("00000000-0000-0000-0000-000000000000");

        public override bool IsValid()
        {
            return true;
        }
    }
}
