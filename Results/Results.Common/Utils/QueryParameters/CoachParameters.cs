using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class CoachParameters : PersonParameters
    {
        public string CoachType { get; set; }

        public override bool IsValid()
        {
            return base.IsValid();
        }
    }
}
