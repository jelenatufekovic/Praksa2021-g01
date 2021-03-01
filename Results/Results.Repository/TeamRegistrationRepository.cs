using System.Collections.Generic;
using Results.Model.Common;
using Results.Model;
using Results.Repository.Common;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils.QueryHelpers;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Results.Common.Utils;
using System.Data;

namespace Results.Repository
{
    public class TeamRegistrationRepository : RepositoryBase, ITeamRegistrationRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public TeamRegistrationRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }
        public TeamRegistrationRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }


        public async Task<bool> CreateTeamRegistrationAsync(ITeamRegistration teamRegistration)
        {
            _command.CommandText = "INSERT INTO TeamRegistration (TeamSeasonID, PlayerID, PositionID, JerseyNumber, ByUser) VALUES (@TeamSeasonID, @PlayerID, @PositionID, @JerseyNumber, @ByUser)";

            _command.Parameters.Clear();

            _command.Parameters.AddWithValue("@TeamSeasonID", teamRegistration.TeamSeasonId);
            _command.Parameters.AddWithValue("@PlayerID", teamRegistration.PlayerId);
            _command.Parameters.AddWithValue("@PositionID", teamRegistration.PositionId);
            _command.Parameters.AddWithValue("@JerseyNumber", teamRegistration.JerseyNumber);
            _command.Parameters.AddWithValue("@ByUser", teamRegistration.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<List<ITeamRegistration>> GetTeamRegistrationsAsync(Guid teamSeasonID, bool includeInactive = false) {
            _command.CommandText = "SELECT * FROM TeamRegistration WHERE TeamSeasonID = @TeamSeasonID AND IsDeleted = 0 ";
            if (!includeInactive) {
                _command.CommandText += "AND IsActive = 1 ";
            }
            _command.Parameters.Clear();

            _command.Parameters.AddWithValue("@TeamSeasonID", teamSeasonID);
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                List<ITeamRegistration> players = new List<ITeamRegistration>();

                while (await reader.ReadAsync())
                {
                    ITeamRegistration player = new TeamRegistration()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        TeamSeasonId = teamSeasonID,
                        PlayerId = Guid.Parse(reader["PlayerID"].ToString()),
                        PositionId = Guid.Parse(reader["PositionID"].ToString()),
                        JerseyNumber = Int32.Parse(reader["JerseyNumber"].ToString()),
                        IsActive = bool.Parse(reader["IsActive"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                    };
                    players.Add(player);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return players;
            }
        }

        public async Task<bool> DeactivateTeamRegistrationAsync(Guid id)
        {
            _command.CommandText = @"UPDATE TeamRegistration SET IsActive = @IsActive, UpdatedAt = @UpdatedAt WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsActive", SqlDbType.Bit).Value = false;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeleteTeamRegistrationAsync(Guid id)
        {
            _command.CommandText = @"UPDATE TeamRegistration SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt WHERE Id = @Id;";

            _command.Parameters.Clear();

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
    }
}
