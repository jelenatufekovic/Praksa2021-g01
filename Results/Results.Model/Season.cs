using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class Season : ModelBase, ISeason
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Period { get; set; }
        public int YearOfStart { get; set; }
    }
}