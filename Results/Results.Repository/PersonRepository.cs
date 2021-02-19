using Results.Common.Utils;
using Results.Model;
using Results.Model.Common;
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
    public class PersonRepository : IPersonRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public PersonRepository(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public PersonRepository(SqlTransaction transaction)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<Guid> CreatePersonAsync(IPerson person)
        {

            _command.CommandText = @"DECLARE @PersonVar table(Id uniqueidentifier);
                                INSERT INTO Person (FirstName, LastName, Country, DateOfBirth) 
                            OUTPUT INSERTED.Id INTO @PersonVar
                                VALUES (@FirstName, @LastName, @Country, @DateOfBirth);
                            SELECT Id FROM @PersonVar;";
            
            _command.Parameters.AddWithValue("@FirstName", person.FirstName);
            _command.Parameters.AddWithValue("@LastName", person.LastName);
            _command.Parameters.AddWithValue("@Country", person.Country);
            _command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
                Guid result = Guid.Parse(reader["Id"].ToString());

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return result;
            }
        }

        public async Task<bool> UpdatePersonAsync(IPerson person)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                _command.CommandText = @"UPDATE Person
                        SET FirstName = @FirstName, LastName = @LastName, Country = @Country, DateOfBirth = @DateOfBirth
                        WHERE Id = @Id;";

                _command.Parameters.AddWithValue("@Id", person.Id);
                _command.Parameters.AddWithValue("@FirstName", person.FirstName);
                _command.Parameters.AddWithValue("@LastName", person.LastName);
                _command.Parameters.AddWithValue("@Country", person.Country);
                _command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);

                bool result = await _command.ExecuteNonQueryAsync() > 0;

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return result;
            }
        }
    }
}
