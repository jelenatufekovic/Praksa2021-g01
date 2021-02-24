using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface ISeason : IModelBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string Period { get; set; }
        int YearOfStart { get; set; }
    }
}
