using Results.Common.Utils;
using Results.Common.Utils.QueryHelpers;
using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;
using Results.Repository.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class PlayerRepository : RepositoryBase, IPlayerRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public PlayerRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public PlayerRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<bool> CreatePlayerAsync(IPlayer player)
        {
            _command.CommandText = "INSERT INTO Player (Id, PlayerValue, ByUser) VALUES (@Id, @PlayerValue, @ByUser)";
            
            _command.Parameters.AddWithValue("@Id", player.Id);
            _command.Parameters.AddWithValue("@PlayerValue", player.PlayerValue);
            _command.Parameters.AddWithValue("@ByUser", player.ByUser);
            
            bool result = await _command.ExecuteNonQueryAsync() > 0;
            
            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeletePlayerAsync(Guid id, Guid ByUser)
        {
            _command.CommandText = "UPDATE Player SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<IPlayer> GetPlayerByIdAsync(Guid id)
        {
            
            _command.CommandText = @"SELECT
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

            if (_command.Transaction != null)
            {
                _command.Parameters.Clear();
            }

            _command.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    IPlayer player = new Player()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Country = reader["Country"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            PlayerValue = Int32.Parse(reader["PlayerValue"].ToString()),
                            ByUser = Guid.Parse(reader["ByUser"].ToString()),
                            IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                            CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                            UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                        };

                    reader.Close();

                    if (_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return player;
                }

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
            }
        }

        public async Task<PagedList<IPlayer>> FindPlayersAsync(PlayerParameters parameters)
        {
            //IQueryHelper<IPlayer, PlayerParameters> _queryHelper = new QueryHelper<IPlayer, PlayerParameters>();
            IQueryHelper<IPlayer, PlayerParameters> _queryHelper = GetQueryHelper<IPlayer, PlayerParameters>();
            
            int totalCount = await GetTableCount<Player>();

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
                                Player.UpdatedAt AS UpdatedAt,
                                AppUser.UserName AS AdminUserName
                             FROM Player 
                             LEFT JOIN Person ON Player.Id = Person.Id
                             LEFT JOIN AppUser ON Player.ByUser = AppUser.Id ";
            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<IPlayer> playerList = new PagedList<IPlayer>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    IPlayer player = new Player()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Country = reader["Country"].ToString(),
                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                        PlayerValue = Int32.Parse(reader["PlayerValue"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString()),
                        AdminUserName = reader["AdminUserName"].ToString(),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                    };
                    playerList.Add(player);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return playerList;
            }
        }

        public async Task<bool> UpdatePlayerAsync(IPlayer player)
        {
            _command.CommandText = "UPDATE Player SET PlayerValue = @PlayerValue, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", player.Id);
            _command.Parameters.AddWithValue("@PlayerValue", player.PlayerValue);
            _command.Parameters.AddWithValue("@ByUser", player.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        //private async Task<int> GetTableCount()
        //{
        //    _command.CommandText = "SELECT COUNT(*) AS TotalCount FROM Player;";
        //    return (Int32)(await _command.ExecuteScalarAsync());
        //}
    }
}
