using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IPlayer : IPerson
    {
        int PlayerValue { get; set; }
    }
}
