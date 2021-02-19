using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class Coach : Person, ICoach
    {
        public string CoachType { get; set; }
    }
}
