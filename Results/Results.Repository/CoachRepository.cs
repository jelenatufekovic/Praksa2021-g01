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
    public class CoachRepository : ICoachRepository
    {
        public async Task<bool> CreateCoachAsync(ICoach coach)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"INSERT INTO Coach (Id, CoachType, ByUser, IsDeleted, CreatedAt, UpdatedAt)
                                VALUES (@Id, @CoachType, @ByUser, @IsDeleted, @CreatedAt, @UpdatedAt)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", coach.Id);
                    command.Parameters.AddWithValue("@CoachType", coach.CoachType);
                    command.Parameters.AddWithValue("@ByUser", coach.UserId);
                    command.Parameters.AddWithValue("@IsDeleted", coach.IsDeleted);
                    command.Parameters.AddWithValue("@CreatedAt", coach.CreatedAt);
                    command.Parameters.AddWithValue("@UpdatedAt", coach.UpdatedAt);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync() > 0);
                }
            }
        }

        public async Task<bool> DeleteCoachAsync(Guid id, Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE Coach
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

        public async Task<ICoach> GetCoachByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"SELECT
                                    Coach.Id AS Id,
                                    Person.FirstName AS FirstName,
                                    Person.LastName AS LastName,
                                    Person.Country AS Country,
                                    Person.DateOfBirth AS DateOfBirth,
                                    Coach.CoachType AS CoachType,
                                    Coach.ByUser AS ByUser,
                                    Coach.IsDeleted AS IsDeleted,
                                    Coach.CreatedAt AS CreatedAt,
                                    Coach.UpdatedAt AS UpdatedAt,
                                FROM Coach 
                                LEFT JOIN Person ON Coach.Id = Person.Id
                                WHERE Coach.Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Coach()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Country = reader["Country"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                CoachType = reader["CoachType"].ToString(),
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

        public async Task<bool> UpdateCoachAsync(ICoach coach)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"
                            UPDATE Coach 
                            SET CoachType = @CoachType, UpdatedAt = @UpdatedAt, ByUser = @ByUser 
                            WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", coach.Id);
                    command.Parameters.AddWithValue("@CoachType", coach.CoachType);
                    command.Parameters.AddWithValue("@UpdatedAt", coach.CreatedAt);
                    command.Parameters.AddWithValue("@ByUser", coach.UserId);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
    }
}
