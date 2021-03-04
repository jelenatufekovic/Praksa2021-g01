using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Model
{
    public class Stadium : ModelBase, IStadium
    {
        public string Name { get; set; }
        public string StadiumAddress { get; set; }
        public int Capacity { get; set; }
        public int YearOfConstruction { get; set; }
        public string Description { get; set; }
    }
}
