using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class RefereeParameters : PersonParameters
    {
        public int Rating { get; set; } = -1;

        public override bool IsValid()
        {
            return base.IsValid() && 
                Rating != -1 ? 
                    Rating > 0 ? 
                        Rating <= 10
                        : false
                    : true;
        }
    }
}
