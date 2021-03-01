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
    public class TeamSeasonRepository : RepositoryBase, ITeamSeasonRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public TeamSeasonRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public TeamSeasonRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<Guid> CreateTeamSeasonAsync(ITeamSeason teamSeason)
        {
            _command.CommandText = @"DECLARE @TeamSeasonVar table(Id uniqueidentifier);
                                INSERT INTO TeamSeason (ClubID, CoachID, LeagueSeasonID, Category, ByUser) 
                                    OUTPUT INSERTED.Id INTO @TeamSeasonVar
                                VALUES (@ClubID, @CoachID, @LeagueSeasonID, @Category, @ByUser); 
                                    SELECT Id FROM @TeamSeasonVar;";

            _command.Parameters.AddWithValue("@ClubID", teamSeason.ClubID);
            _command.Parameters.AddWithValue("@CoachID", teamSeason.CoachID);
            _command.Parameters.AddWithValue("@LeagueSeasonID", teamSeason.LeagueSeasonID);
            _command.Parameters.AddWithValue("@Category", teamSeason.Category);
            _command.Parameters.AddWithValue("@ByUser", teamSeason.ByUser);

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

        public async Task<List<ITeamSeason>> GetTeamSeasonAsync(Guid clubID)
        { 
            _command.CommandText = @"SELECT * FROM TeamSeason WHERE ClubID = @ClubID ";

            _command.Parameters.Clear();
            _command.Parameters.AddWithValue("@ClubID", clubID);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                List<ITeamSeason> teamSeasonList = new List<ITeamSeason>();

                while (await reader.ReadAsync())
                {
                    ITeamSeason teamSeason = new TeamSeason()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        ClubID = Guid.Parse(reader["ClubID"].ToString()),
                        CoachID = Guid.Parse(reader["CoachID"].ToString()),
                        LeagueSeasonID = Guid.Parse(reader["LeagueSeasonID"].ToString()),
                        Category = reader["Category"].ToString(),
                        ByUser = Guid.Parse(reader["ByUser"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                    };
                    teamSeasonList.Add(teamSeason);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return teamSeasonList;
            }
        }
        public async Task<bool> DeleteTeamSeasonAsync(Guid id)
        {
            _command.CommandText = @"UPDATE TeamSeason SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt WHERE Id = @Id;";

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
