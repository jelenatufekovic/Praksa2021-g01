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
    public class RefereeRepository : IRefereeRepository
    {
        public async Task<bool> CreateRefereeAsync(IReferee referee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"INSERT INTO Referee (Id, Rating, ByUser, IsDeleted, CreatedAt, UpdatedAt)
                                VALUES (@Id, @Rating, @ByUser, @IsDeleted, @CreatedAt, @UpdatedAt)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", referee.Id);
                    command.Parameters.AddWithValue("@Rating", referee.Rating);
                    command.Parameters.AddWithValue("@ByUser", referee.UserId);
                    command.Parameters.AddWithValue("@IsDeleted", referee.IsDeleted);
                    command.Parameters.AddWithValue("@CreatedAt", referee.CreatedAt);
                    command.Parameters.AddWithValue("@UpdatedAt", referee.UpdatedAt);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync() > 0);
                }
            }
        }

        public async Task<bool> DeleteRefereeAsync(Guid id, Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE Referee
                        SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                        WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", userId);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<IReferee> GetRefereeByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"SELECT
                                    Referee.Id AS Id,
                                    Person.FirstName AS FirstName,
                                    Person.LastName AS LastName,
                                    Person.Country AS Country,
                                    Person.DateOfBirth AS DateOfBirth,
                                    Referee.Rating AS Rating,
                                    Referee.ByUser AS ByUser,
                                    Referee.IsDeleted AS IsDeleted,
                                    Referee.CreatedAt AS CreatedAt,
                                    Referee.UpdatedAt AS UpdatedAt,
                                FROM Referee 
                                LEFT JOIN Person ON Referee.Id = Person.Id
                                WHERE Referee.Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Referee()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Country = reader["Country"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                Rating = Int32.Parse(reader["Rating"].ToString()),
                                UserId = Guid.Parse(reader["ByUser"].ToString()),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<bool> UpdateRefereeAsync(IReferee referee)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"
                            UPDATE Referee 
                            SET Rating = @Rating, UpdatedAt = @UpdatedAt, ByUser = @ByUser 
                            WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", referee.Id);
                    command.Parameters.AddWithValue("@Rating", referee.Rating);
                    command.Parameters.AddWithValue("@UpdatedAt", referee.CreatedAt);
                    command.Parameters.AddWithValue("@ByUser", referee.UserId);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
    }
}
