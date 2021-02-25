using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Repository.Common;
using Results.Model.Common;
using System.Data.SqlClient;
using System.Data;
using Results.Model;

namespace Results.Repository
{
    public class ClubRepository : RepositoryBase, IClubRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public ClubRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public ClubRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }
        public async Task<bool> CreateClubAsync(IClub club) 
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"INSERT INTO Club(StadiumID, Name, ClubAddress, ShortName, YearOfFoundation, Description, CreatedAt, UpdatedAt, IsDeleted, ByUser)
                                VALUES(@StadiumID, @Name, @ClubAddress, @ShortName, @YearOfFoundation, @Description, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser);";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StadiumID", club.StadiumID);
                    command.Parameters.AddWithValue("@Name", club.Name);
                    command.Parameters.AddWithValue("@ClubAddress", club.ClubAddress);
                    command.Parameters.AddWithValue("@ShortName", club.ShortName);
                    command.Parameters.AddWithValue("@YearOfFoundation", club.YearOfFoundation);
                    command.Parameters.AddWithValue("@Description", club.Description);
                    command.Parameters.AddWithValue("@CreatedAt", club.CreatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", club.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", club.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
        public async Task<bool> UpdateClubAsync(IClub club)
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"UPDATE Club 
                SET Name = @Name, ClubAddress = @ClubAddress, ShortName = @ShortName, Description = @Description, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", club.Id);
                    command.Parameters.AddWithValue("@Name", club.Name);
                    command.Parameters.AddWithValue("@ClubAddress", club.ClubAddress);
                    command.Parameters.AddWithValue("@ShortName", club.ShortName);
                    command.Parameters.AddWithValue("@Description", club.Description);
                    command.Parameters.AddWithValue("@UpdatedAt", club.UpdatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", club.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
        public async Task<bool> DeleteClubAsync(IClub club)
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"UPDATE Club
                SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", club.Id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", club.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
        public async Task<List<IClub>> GetAllClubsAsync()
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = "SELECT Name, ClubAddress, ShortName, YearOfFoundation, Description" +
                               " FROM Club WHERE IsDeleted = @IsDeleted;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<IClub> clubs = new List<IClub>();
                        while (await reader.ReadAsync())
                        {
                            IClub club = new Club()
                            {
                                Name = reader["Name"].ToString(),
                                ClubAddress = reader["ClubAddress"].ToString(),
                                ShortName = reader["ShortName"].ToString(),
                                YearOfFoundation = DateTime.Parse(reader["YearOfFoundation"].ToString()),
                                Description = reader["Description"].ToString()
                            };
                            clubs.Add(club);
                        }
                        return clubs;
                    }
                }
            }
        }
        public async Task<IClub> GetClubByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"SELECT * FROM Club WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Club()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                StadiumID = Guid.Parse(reader["StadiumID"].ToString()),
                                Name = reader["Name"].ToString(),
                                ClubAddress = reader["ClubAddress"].ToString(),
                                ShortName = reader["ShortName"].ToString(),
                                YearOfFoundation = DateTime.Parse(reader["YearOfFoundation"].ToString()),
                                Description = reader["Description"].ToString(),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                                ByUser = Guid.Parse(reader["ByUser"].ToString())
                            };
                        }
                        return null;
                    }
                }
            }
        }
        public async Task<IClub> GetClubByNameAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"SELECT * FROM Club WHERE Name = @name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Club()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                StadiumID = Guid.Parse(reader["StadiumID"].ToString()),
                                Name = reader["Name"].ToString(),
                                ClubAddress = reader["ClubAddress"].ToString(),
                                ShortName = reader["ShortName"].ToString(),
                                YearOfFoundation = DateTime.Parse(reader["YearOfFoundation"].ToString()),
                                Description = reader["Description"].ToString(),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                                ByUser = Guid.Parse(reader["ByUser"].ToString())
                            };
                        }
                        return null;
                    }
                }
            }
        }


        public async Task<IClub> GetClubByStadiumIDAsync(Guid StadiumID)
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"SELECT * FROM Club WHERE StadiumID = @StadiumID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StadiumID", StadiumID);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Club()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                StadiumID = Guid.Parse(reader["StadiumID"].ToString()),
                                Name = reader["Name"].ToString(),
                                ClubAddress = reader["ClubAddress"].ToString(),
                                ShortName = reader["ShortName"].ToString(),
                                YearOfFoundation = DateTime.Parse(reader["YearOfFoundation"].ToString()),
                                Description = reader["Description"].ToString(),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                                ByUser = Guid.Parse(reader["ByUser"].ToString())
                            };
                        }
                        return null;
                    }
                }
            }
        }
    }
}
