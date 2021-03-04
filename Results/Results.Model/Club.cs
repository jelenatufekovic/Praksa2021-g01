using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;

namespace Results.Model
{
    public class Club : ModelBase, IClub
    {
        public Guid StadiumID { get; set; }
        public string Name { get; set; }
        public string ClubAddress { get; set; }
        public string ShortName { get; set; }
        public int YearOfFoundation { get; set; }
        public string Description { get; set; }
        public IStadium Stadium { get; set; }
    }
}
