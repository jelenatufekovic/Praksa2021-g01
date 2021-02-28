using Results.Model.Common;
using Results.Repository.Common;
using Results.Model;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils.QueryHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Results.Common.Utils;
using System.Data.SqlClient;

namespace Results.Repository
{
    public class MatchRepository : IMatchRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public MatchRepository(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public MatchRepository(SqlTransaction transaction)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<Guid> CreateMatchAsync(IMatch match)
        {
            _command.CommandText = @"DECLARE @MatchVar table(Id uniqueidentifier);
								INSERT INTO Match (HomeTeamSeasonID, AwayTeamSeasonID, LeagueSeasonID, RefereeID, MatchDate, MatchDay, IsPlayed, CreatedAt, UpdatedAt, IsDeleted, ByUser)
								OUTPUT INSERTED.Id INTO @MatchVar
								VALUES (@HomeTeamSeasonID, @AwayTeamSeasonID, @LeagueSeasonID, @RefereeID, @MatchDate, @MatchDay, @IsPlayed, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser);
								SELECT Id FROM @MatchVar;";  //ako bude error pogledat prvo ;

            _command.Parameters.AddWithValue("@HomeTeamSeasonID", match.HomeTeamSeasonID);
            _command.Parameters.AddWithValue("@AwayTeamSeasonID", match.AwayTeamSeasonID);
            _command.Parameters.AddWithValue("@LeagueSeasonID", match.LeagueSeasonID);
            _command.Parameters.AddWithValue("@RefereeID", match.RefereeID);
            _command.Parameters.AddWithValue("@MatchDate", match.MatchDate);
            _command.Parameters.AddWithValue("@MatchDay", match.MatchDay);
            _command.Parameters.Add("@IsPlayed", SqlDbType.Bit).Value = false;
            _command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
            _command.Parameters.AddWithValue("@ByUser", match.ByUser);

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

        public async Task<bool> CheckMatchExistingAsync(MatchParameters parameters)
        {
            IQueryHelper<IMatch, MatchParameters> queryHelper = new QueryHelper<IMatch, MatchParameters>();

            string query = @"SELECT * FROM Match";
            query += queryHelper.Filter.ApplyFilters(parameters);

            _command.CommandText = query;

            bool result = await _command.ExecuteNonQueryAsync() > 0;
            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeleteMatchAsync(Guid id, Guid ByUser)
        {
            _command.CommandText = "UPDATE Match SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

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

        public async Task<bool> UpdateMatchAsync(IMatch match)
        {
            _command.CommandText = "UPDATE Match SET RefereeID = @RefereeID, MatchDate = @MatchDate, MatchDay = @MatchDay, IsPlayed = @IsPlayed, @UpdatedAt = UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", match.Id);
            _command.Parameters.AddWithValue("@RefereeID", match.RefereeID);
            _command.Parameters.AddWithValue("@MatchDate", match.MatchDate);
            _command.Parameters.AddWithValue("@MatchDay", match.MatchDay);
            _command.Parameters.AddWithValue("@IsPlayed", match.IsPlayed);
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", match.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<IMatch> GetMatchByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                _command.CommandText = @"SELECT
                                Match.Id AS Id,
                                homeclub.Name AS HomeTeam,
                                awayclub.Name AS AwayTeam,
                                Match.LeagueSeasonID AS LeagueSeason,
                                Referee.Name AS Referee,
                                MatchDate, MatchDay, IsPlayed,
                                ByUser, IsDeleted, CreatedAt, UpdatedAt
								FROM Match
								LEFT JOIN TeamSeason hometeam ON Match.HomeTeamSeasonID = hometeam.Id
								LEFT JOIN TeamSeason awayteam ON Match.AwayTeamSeasonID = awayteam.Id
								LEFT JOIN Club homeclub ON hometeam.ClubID = homeclub.Id
								LEFT JOIN Club awayclub ON awayteam.ClubID = awayclub.Id
								LEFT JOIN Referee ON Match.RefereeID = Referee.Id
								WHERE Match.Id = @Id;";

                _command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = await _command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        IMatch match = new Match()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            HomeTeam = reader["HomeTeam"].ToString(),
                            AwayTeam = reader["AwayTeam"].ToString(),
                            LeagueSeasonID = Guid.Parse(reader["LeagueSeason"].ToString()),
                            RefereeName = reader["Referee"].ToString(),
                            MatchDate = DateTime.Parse(reader["MatchDate"].ToString()),
                            MatchDay = Convert.ToInt32(reader["MatchDay"].ToString()),
                            IsPlayed = bool.Parse(reader["IsPlayed"].ToString()),
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

                        return match;
                    }

                    if (_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return null;
                }
            }
        }

        //private async Task<int> GetTableCount()
        //{
        //    _command.CommandText = "SELECT COUNT(*) AS TotalCount FROM Match;";
        //    return (Int32)(await _command.ExecuteScalarAsync());
        //}
    }

}
