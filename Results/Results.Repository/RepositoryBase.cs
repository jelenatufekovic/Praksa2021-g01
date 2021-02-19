using Results.Common.Utils.QueryHelpers;
using Results.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository
{
    public abstract class RepositoryBase : IRepositoryBase
    {
        private readonly SqlCommand _command;

        public RepositoryBase(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
        }

        public RepositoryBase(SqlTransaction transaction)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public IQueryHelper<T, K> GetQueryHelper<T, K>()
        {
            return Activator.CreateInstance(typeof(QueryHelper<T, K>)) as IQueryHelper<T, K>;
        }

        public async Task<int> GetTableCount<T>()
        {
            _command.CommandText = $"SELECT COUNT(*) AS TotalCount FROM {typeof(T).Name};";
            return (Int32)(await _command.ExecuteScalarAsync());
        }
    }
}
