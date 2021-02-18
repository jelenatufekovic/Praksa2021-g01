﻿using Results.Common.Utils;
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
        private SqlConnection _connection;
        private SqlCommand _command;

        public PlayerRepository(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public PlayerRepository(SqlTransaction transaction)
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

        //public async Task<PagedList<IPlayer>> GetPlayersByQuery(PlayerParameters parameters)
        //{
        //    string query = @"SELECT 
        //                        totalCount COUNT(*) OVER(),
        //                        Player.Id AS Id,
        //                        Person.FirstName AS FirstName,
        //                        Person.LastName AS LastName,
        //                        Person.Country AS Country,
        //                        Person.DateOfBirth AS DateOfBirth,
        //                        Player.PlayerValue AS PlayerValue,
        //                        Player.ByUser AS ByUser,
        //                        Player.IsDeleted AS IsDeleted,
        //                        Player.CreatedAt AS CreatedAt,
        //                        Player.UpdatedAt AS UpdatedAt
        //                    FROM Player 
        //                    LEFT JOIN Person ON Player.Id = Person.Id";
        //    query += 

        //}
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
    }
}
