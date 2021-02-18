using Results.Common.Utils;
using Results.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly SqlConnection _connection;

        public RepositoryFactory()
        {
            _connection = new SqlConnection(ConnectionString.GetDefaultConnectionString());
        }

        public T GetRepository<T>() where T : class
        {
            return Activator.CreateInstance(typeof(T), _connection) as T;
        }

        public IUnitOfWork GetUnitOfWork()
        {
            return new UnitOfWork(_connection);
        }

    }
}
