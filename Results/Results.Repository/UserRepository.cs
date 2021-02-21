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
        private SqlConnection _connection;
        private SqlCommand _command;

        public UserRepository(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public UserRepository(SqlTransaction transaction)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<Guid> CreateUserAsync(IUser user)
        {
            _command.CommandText = @"DECLARE @AppUserVar table(Id uniqueidentifier);
                                INSERT INTO AppUser (FirstName, LastName, Email, UserName, Salt, PasswordHash, IsAdmin) 
                                    OUTPUT INSERTED.Id INTO @AppUserVar
                                VALUES (@FirstName, @LastName, @Email, @UserName, @Salt, @PasswordHash, @IsAdmin);
                                    SELECT Id FROM @AppUserVar;";

            _command.Parameters.AddWithValue("@FirstName", user.FirstName);
            _command.Parameters.AddWithValue("@LastName", user.LastName);
            _command.Parameters.AddWithValue("@Email", user.Email);
            _command.Parameters.AddWithValue("@UserName", user.UserName);
            _command.Parameters.AddWithValue("@Salt", user.Salt);
            _command.Parameters.AddWithValue("@PasswordHash", user.PasswordHash);
            _command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);

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

        public async Task<bool> DeleteUserAsync(Guid id)
        {
            _command.CommandText = @"UPDATE AppUser SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<IUser> GetUserByIdAsync(Guid id)
        {
            _command.CommandText = "SELECT * FROM AppUser WHERE Id = @Id AND IsDeleted = @IsDeleted;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    IUser user = new User()
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
                    reader.Close();

                    if (_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return user;
                }

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
            }
        }

        public async Task<IUser> GetUserByEmailAsync(string email)
        {
            _command.CommandText = "SELECT * FROM AppUser WHERE Email = @Email";

            _command.Parameters.AddWithValue("@Email", email);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    IUser user = new User()
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
                    reader.Close();

                    if (_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return user;
                }

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
            }
        }

        public async Task<IUser> GetUserByUserNameAsync(string userName)
        {
            _command.CommandText = "SELECT * FROM AppUser WHERE UserName = @UserName;";

            _command.Parameters.AddWithValue("@UserName", userName);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    IUser user = new User()
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
                    reader.Close();

                    if (_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return user;
                }

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
            }
        }

        public async Task<bool> RestoreUserAsync(string email)
        {
            _command.CommandText = "UPDATE AppUser SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt WHERE Email = @Email;";

            _command.Parameters.AddWithValue("@Email", email);
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> UpdateUserAsync(IUser user)
        {
            _command.CommandText = "UPDATE AppUser SET FirstName = @FirstName, LastName = @LastName, Email = @Email, IsAdmin = @IsAdmin, UpdatedAt = @UpdatedAt WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", user.Id);
            _command.Parameters.AddWithValue("@FirstName", user.FirstName);
            _command.Parameters.AddWithValue("@LastName", user.LastName);
            _command.Parameters.AddWithValue("@Email", user.Email);
            _command.Parameters.AddWithValue("@IsAdmin", user.IsAdmin);
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        
    }
}
