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
        public async Task<Guid> CreatePersonAsync(IPerson person)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"DECLARE @PersonVar table(Id uniqueidentifier);
                                    INSERT INTO Person (FirstName, LastName, Country, DateOfBirth) 
                                OUTPUT INSERTED.Id INTO @PersonVar
                                    VALUES (@FirstName, @LastName, @Country, @DateOfBirth);
                                SELECT Id FROM @PersonVar;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@Country", person.Country);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);

                    await connection.OpenAsync();
                    using (SqlDataReader result = await command.ExecuteReaderAsync())
                    {
                        await result.ReadAsync();
                        return Guid.Parse(result["Id"].ToString());
                    }
                }
            }
        }

        public async Task<bool> UpdatePersonAsync(IPerson person)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE Person
                        SET FirstName = @FirstName, LastName = @LastName, Country = @Country, DateOfBirth = @DateOfBirth
                        WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", person.Id);
                    command.Parameters.AddWithValue("@FirstName", person.FirstName);
                    command.Parameters.AddWithValue("@LastName", person.LastName);
                    command.Parameters.AddWithValue("@Country", person.Country);
                    command.Parameters.AddWithValue("@DateOfBirth", person.DateOfBirth);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
    }
}
