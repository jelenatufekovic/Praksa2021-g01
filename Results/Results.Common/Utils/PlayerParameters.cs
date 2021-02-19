using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public class PlayerParameters : PersonParameters
    {
        const int minPlayerValue = 1000;
        public PlayerParameters()
        {
            OrderBy = "LastName";
        }

        public int MinPlayerValue { get; set; } = -1;
        public int MaxPlayerValue { get; set; } = -1;

        public override bool IsValid()
        {
            return (base.IsValid() && 
                (MaxPlayerValue > 0 ? MinPlayerValue <= MaxPlayerValue && MaxPlayerValue < Int32.MaxValue : true ) &&
                (MinPlayerValue > 0 ? minPlayerValue <= MinPlayerValue : true));
        }
    }
}
