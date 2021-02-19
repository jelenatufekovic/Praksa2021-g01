using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IRepositoryFactory
    {
        T GetRepository<T>() where T : class;
         IUnitOfWork GetUnitOfWork();
    }
}
