using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Common.Utils.QueryParameters
{
    public class PositionParameters : QueryParameters
    {
        public PositionParameters()
        {
            OrderBy = "Id";
            Name = "";
            ShortName = "";
        }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public override bool IsValid() {
            return base.IsValid() && Name.All(Char.IsLetter) && ShortName.All(Char.IsLetter);
        }
    }
}
