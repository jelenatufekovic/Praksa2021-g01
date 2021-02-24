using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface ILeague : IModelBase
    {
        Guid Id { get; set; }
        string Name { get; set; }
        string ShortName { get; set; }
        int Rank { get; set; }
        string Country { get; set; }
    }
}
