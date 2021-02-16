using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class Player : ModelBase, IPlayer
    {
        public Guid PersonId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirt { get; set; }
        public string Country { get; set; }
        public int Value { get; set; }
    }
}
