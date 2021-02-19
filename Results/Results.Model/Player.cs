using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class Player : Person, IPlayer
    {
        public int PlayerValue { get; set; }
    }
}
