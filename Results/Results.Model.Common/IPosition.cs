using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IPosition : IModelBase
    {
        string Name { get; set; }

        string ShortName { get; set; }

    }
}
