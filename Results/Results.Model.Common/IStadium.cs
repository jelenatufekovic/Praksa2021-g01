using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IStadium : IModelBase
    {
        string Name { get; set; }
        string StadiumAddress { get; set; }
        int Capacity { get; set; }
        int YearOfConstruction { get; set; }
        string Description { get; set; }
    }
}
