using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IClub : IModelBase
    {
        Guid StadiumID { get; set; }
        string Name { get; set; }
        string ClubAddress { get; set; }
        string ShortName { get; set; }
        int YearOfFoundation { get; set; }
        string Description { get; set; }
        IStadium Stadium { get; set; }
    }
}
