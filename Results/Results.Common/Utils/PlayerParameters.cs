using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils
{
    public class PlayerParameters : PersonParameters
    {
        //public int PlayerValue { get; set; }
        public PlayerParameters()
        {
            OrderBy = "LastName";
        }
    }
}
