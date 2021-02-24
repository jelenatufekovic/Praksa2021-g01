using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model.Common
{
    public interface IModelBase
    {
        Guid Id { get; set; }
        DateTime CreatedAt { get; set; }
        DateTime UpdatedAt { get; set; }
        bool IsDeleted { get; set; }
        Guid ByUser { get; set; }
        string AdminUserName { get; set; }
    }
}
