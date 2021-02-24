using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class PlayerParameters : PersonParameters
    {
        const int minPlayerValue = 1000;
        public PlayerParameters()
        {
            OrderBy = "LastName";
        }

        public int? MinPlayerValue { get; set; }
        public int? MaxPlayerValue { get; set; }

        public override bool IsValid()
        {
            return (base.IsValid() &&
                MinPlayerValue == null ?
                    true :
                    MinPlayerValue > 0 ?
                        MinPlayerValue >= minPlayerValue : false
                    &&
                MaxPlayerValue == null ?
                    true :
                    MaxPlayerValue > 0 ?
                        MaxPlayerValue < Int32.MaxValue : false
                    &&
                MaxPlayerValue > 0 && MinPlayerValue > 0 ?
                    MaxPlayerValue >= MinPlayerValue : false);
                
        }
    }
}
