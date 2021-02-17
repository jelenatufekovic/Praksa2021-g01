using Results.Common.Utils;
using Results.Model;
using Results.Model.Common;
using Results.Repository.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class PlayerRepository : IPlayerRepository
    {
        public async Task<bool> CreatePlayerAsync(IPlayer player)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "INSERT INTO Player (Id, PlayerValue, ByUser) VALUES (@Id, @PlayerValue, @ByUser)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", player.Id);
                    command.Parameters.AddWithValue("@PlayerValue", player.PlayerValue);
                    command.Parameters.AddWithValue("@ByUser", player.UserId);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync() > 0);
                }
            }
        }

        public async Task<bool> DeletePlayerAsync(Guid id, Guid userId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE Player
                        SET IsDeleted = @IsDeleted, ByUser = @ByUser
                        WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@ByUser", userId);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<IPlayer> GetPlayerByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"SELECT
                                    Player.Id AS Id,
                                    Person.FirstName AS FirstName,
                                    Person.LastName AS LastName,
                                    Person.Country AS Country,
                                    Person.DateOfBirth AS DateOfBirth,
                                    Player.PlayerValue AS PlayerValue,
                                    Player.ByUser AS ByUser,
                                    Player.IsDeleted AS IsDeleted,
                                    Player.CreatedAt AS CreatedAt,
                                    Player.UpdatedAt AS UpdatedAt
                                FROM Player 
                                LEFT JOIN Person ON Player.Id = Person.Id
                                WHERE Player.Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Player()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Country = reader["Country"].ToString(),
                                DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                                PlayerValue = Int32.Parse(reader["PlayerValue"].ToString()),
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

        public async Task<bool> UpdatePlayerAsync(IPlayer player)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"
                            UPDATE Player 
                            SET PlayerValue = @PlayerValue, ByUser = @ByUser 
                            WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", player.Id);
                    command.Parameters.AddWithValue("@PlayerValue", player.PlayerValue);
                    command.Parameters.AddWithValue("@ByUser", player.UserId);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
    }
}
