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
        public async Task<Guid> CreateMatchAsync(IMatch match)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"DECLARE @MatchVar table(Id uniqueidentifier);
							INSERT INTO Match (HomeTeamSeasonID, AwayTeamSeasonID, LeagueSeasonID, RefereeID, MatchDate, MatchDay, IsPlayed, CreatedAt, UpdatedAt, IsDeleted, ByUser)
							OUTPUT INSERTED.Id INTO @MatchVar
							VALUES (@HomeTeamSeasonID, @AwayTeamSeasonID, @LeagueSeasonID, @RefereeID, @MatchDate, @MatchDay, @IsPlayed, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser);
							SELECT Id FROM @MatchVar;";  //ako bude error pogledat prvo ;

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HomeTeamSeasonID", match.HomeTeamSeasonID);
                    command.Parameters.AddWithValue("@AwayTeamSeasonID", match.AwayTeamSeasonID);
                    command.Parameters.AddWithValue("@LeagueSeasonID", match.LeagueSeasonID);
                    command.Parameters.AddWithValue("@RefereeID", match.RefereeID);
                    command.Parameters.AddWithValue("@MatchDate", match.MatchDate);
                    command.Parameters.AddWithValue("@MatchDay", match.MatchDay);
                    command.Parameters.Add("@IsPlayed", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@CreatedAt", match.CreatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", match.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", match.ByUser); 

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        await reader.ReadAsync();
                        Guid result = Guid.Parse(reader["Id"].ToString());
                        return result;
                    }
                }
            }
        }

        public async Task<bool> CheckMatchExistingAsync(MatchParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                IQueryHelper<IMatch, MatchParameters> queryHelper = new QueryHelper<IMatch, MatchParameters>();

                string query = @"SELECT * FROM Match ";
                query += queryHelper.Filter.ApplyFilters(parameters);

                await connection.OpenAsync();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        return await reader.ReadAsync();
                    }
                }
            }
        }

        public async Task<bool> DeleteMatchAsync(Guid id, Guid ByUser)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "UPDATE Match SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<bool> UpdateMatchAsync(IMatch match)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "UPDATE Match SET RefereeID = @RefereeID, MatchDate = @MatchDate, MatchDay = @MatchDay, IsPlayed = @IsPlayed, @UpdatedAt = UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", match.Id);
                    command.Parameters.AddWithValue("@RefereeID", match.RefereeID);
                    command.Parameters.AddWithValue("@MatchDate", match.MatchDate);
                    command.Parameters.AddWithValue("@MatchDay", match.MatchDay);
                    command.Parameters.AddWithValue("@IsPlayed", match.IsPlayed);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", match.ByUser);

                    await connection.OpenAsync();
                    try
                    {
                        return (await command.ExecuteNonQueryAsync()) > 0;
                    }
                    catch(SqlException)
                    {
                        return false;
                    }
                    
                }
            }
        }

        public async Task<IMatch> GetMatchByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"SELECT
                                Match.Id AS Id,
                                homeclub.Name AS HomeTeam,
                                awayclub.Name AS AwayTeam,
                                Match.LeagueSeasonID AS LeagueSeason,
                                person.FirstName AS Referee,
                                MatchDate, MatchDay, IsPlayed
                                FROM Match
                                LEFT JOIN TeamSeason hometeam ON Match.HomeTeamSeasonID = hometeam.Id
                                LEFT JOIN TeamSeason awayteam ON Match.AwayTeamSeasonID = awayteam.Id
                                LEFT JOIN Club homeclub ON hometeam.ClubID = homeclub.Id
                                LEFT JOIN Club awayclub ON awayteam.ClubID = awayclub.Id
                                LEFT JOIN Referee ref ON Match.RefereeID = ref.Id
                                LEFT JOIN Person person ON ref.Id = person.Id
                                WHERE Match.Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    IMatch match = null;

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            match = new Match()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                HomeTeam = reader["HomeTeam"].ToString(),
                                AwayTeam = reader["AwayTeam"].ToString(),
                                LeagueSeasonID = Guid.Parse(reader["LeagueSeason"].ToString()),
                                RefereeName = reader["Referee"].ToString(),
                                MatchDate = DateTime.Parse(reader["MatchDate"].ToString()),
                                MatchDay = Convert.ToInt32(reader["MatchDay"].ToString()),
                                IsPlayed = bool.Parse(reader["IsPlayed"].ToString())
                            };
                            reader.Close();
                        }
                        return match;
                    }
                }
            }
        }

        public async Task<PagedList<IMatch>> GetMatchByQueryAsync(MatchQueryParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"SELECT
                                Match.Id AS Id,
                                homeclub.Name AS HomeTeam,
                                awayclub.Name AS AwayTeam,
                                Match.LeagueSeasonID AS LeagueSeason,
                                person.FirstName AS Referee,
                                MatchDate, MatchDay, IsPlayed
                                FROM Match
                                LEFT JOIN TeamSeason hometeam ON Match.HomeTeamSeasonID = hometeam.Id
                                LEFT JOIN TeamSeason awayteam ON Match.AwayTeamSeasonID = awayteam.Id
                                LEFT JOIN Club homeclub ON hometeam.ClubID = homeclub.Id
                                LEFT JOIN Club awayclub ON awayteam.ClubID = awayclub.Id
                                LEFT JOIN Referee ref ON Match.RefereeID = ref.Id
                                LEFT JOIN Person person ON ref.Id = person.Id ";

                IQueryHelper<IMatch, MatchQueryParameters> queryHelper = new QueryHelper<IMatch, MatchQueryParameters>();

                int totalCount = await GetTableCount(connection);

                string statement = queryHelper.Filter.ApplyFilters(parameters);
                if (statement.Contains("LeagueSeasonID")) statement = statement.Replace("LeagueSeasonID", "Match.LeagueSeasonID");
                query += statement;
                query += queryHelper.Sort.ApplySort(parameters.OrderBy);
                query += queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {

                        PagedList<IMatch> matchList = new PagedList<IMatch>(totalCount, parameters.PageNumber, parameters.PageSize);

                        while (await reader.ReadAsync())
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
                                IsPlayed = bool.Parse(reader["IsPlayed"].ToString())
                            };
                            matchList.Add(match);
                        }
                        reader.Close();
                        return matchList;
                    }
                }
            }
        }

        private async Task<int> GetTableCount(SqlConnection connection)
        {
            string query = "SELECT COUNT(*) AS TotalCount FROM Match;";
            SqlCommand command = new SqlCommand(query, connection);
            await connection.OpenAsync();
            return (Int32)(await command.ExecuteScalarAsync());
        }
    }

}
