using Results.Model.Common;
using Results.Repository.Common;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using System.Data.SqlClient;
using Results.Model;

namespace Results.Repository
{
    public class UserRepository : IUserRepository
    {
        public UserRepository()
        {
        }

        public async Task<Guid> CreateUserAsync(IUser user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"DECLARE @AppUserVar table(Id uniqueidentifier);
                                INSERT INTO AppUser (FirstName, LastName, Email, UserName, IsAdmin) 
                            OUTPUT INSERTED.Id INTO @AppUserVar
                                VALUES (@FirstName, @LastName, @Email, @UserName, @IsAdmin); 
                            SELECT Id FROM @AppUserVar;";
                
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@UserName", user.UserName);
                    //command.Parameters.AddWithValue("@Salt", user.Salt);
                    //command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
                    command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);

                    await connection.OpenAsync();
                    using (SqlDataReader result = await command.ExecuteReaderAsync())
                    {
                        await result.ReadAsync();
                        return Guid.Parse(result["Id"].ToString());
                    }

                }
            }
        }

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE AppUser SET IsDeleted = @IsDeleted WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<IUser> GetUserByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "SELECT * FROM AppUser WHERE Id = @Id AND IsDeleted = @IsDeleted;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Salt = reader["Salt"].ToString(),
                                IsAdmin = bool.Parse(reader["IsAdmin"].ToString()),
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

        public async Task<IUser> GetUserByEmailAsync(string email)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "SELECT * FROM AppUser WHERE Email = @Email";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    
                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Salt = reader["Salt"].ToString(),
                                IsAdmin = bool.Parse(reader["IsAdmin"].ToString()),
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

        public async Task<IUser> GetUserByUserNameAsync(string userName)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "SELECT * FROM AppUser WHERE UserName = @UserName;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new User()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                UserName = reader["UserName"].ToString(),
                                Email = reader["Email"].ToString(),
                                PasswordHash = reader["PasswordHash"].ToString(),
                                Salt = reader["Salt"].ToString(),
                                IsAdmin = bool.Parse(reader["IsAdmin"].ToString()),
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

        public async Task<bool> UpdateUserAsync(IUser user)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE AppUser
                                SET FirstName = @FirstName, LastName = @LastName, Email = @Email, IsAdmin = @IsAdmin
                                WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", user.Id);
                    command.Parameters.AddWithValue("@FirstName", user.FirstName);
                    command.Parameters.AddWithValue("@LastName", user.LastName);
                    command.Parameters.AddWithValue("@Email", user.Email);
                    command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
    }
}
