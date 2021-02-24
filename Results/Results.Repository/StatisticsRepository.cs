using Results.Common.Utils;
using Results.Common.Utils.QueryHelpers;
using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;
using Results.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class StatisticsRepository : IStatisticsRepository
    {
        public async Task<bool> CreateStatisticsAsync(IStatistics statistics)
        {
            using(SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"insert into MatchStatistics (MatchId, 
                                                              HomeTeamSeasonID,
                                                              AwayTeamSeasonID,
                                                              HomeGoals, 
                                                              AwayGoals, 
                                                              HomeYellowCards, 
                                                              AwayYellowCards, 
                                                              HomeRedCards, 
                                                              AwayRedCards, 
                                                              HomeShots, 
                                                              AwayShots, 
                                                              HomeShotsOnTarget, 
                                                              AwayShotsOnTarget, 
                                                              HomePossession, 
                                                              AwayPossession, 
                                                              IsDeleted, 
                                                              ByUser) 
                                                      values (@MatchId, 
                                                              @HomeTeamSeasonID,
                                                              @AwayTeamSeasonID,
                                                              @HomeGoals, 
                                                              @AwayGoals, 
                                                              @HomeYellowCards, 
                                                              @AwayYellowCards, 
                                                              @HomeRedCards, 
                                                              @AwayRedCards, 
                                                              @HomeShots, 
                                                              @AwayShots, 
                                                              @HomeShotsOnTarget, 
                                                              @AwayShotsOnTarget, 
                                                              @HomePossession, 
                                                              @AwayPossession, 
                                                              @IsDeleted, 
                                                              @ByUser)";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MatchId", statistics.MatchId);
                    command.Parameters.AddWithValue("@HomeTeamSeasonID", statistics.HomeTeamSeasonId);
                    command.Parameters.AddWithValue("@AwayTeamSeasonID", statistics.AwayTeamSeasonId);
                    command.Parameters.AddWithValue("@HomeGoals", statistics.HomeGoals);
                    command.Parameters.AddWithValue("@AwayGoals", statistics.AwayGoals);
                    command.Parameters.AddWithValue("@HomeYellowCards", statistics.HomeYellowCards);
                    command.Parameters.AddWithValue("@AwayYellowCards", statistics.AwayYellowCards);
                    command.Parameters.AddWithValue("@HomeRedCards", statistics.HomeRedCards);
                    command.Parameters.AddWithValue("@AwayRedCards", statistics.AwayRedCards);
                    command.Parameters.AddWithValue("@HomeShots", statistics.HomeShots);
                    command.Parameters.AddWithValue("@AwayShots", statistics.AwayShots);
                    command.Parameters.AddWithValue("@HomeShotsOnTarget", statistics.HomeShotsOnTarget);
                    command.Parameters.AddWithValue("@AwayShotsOnTarget", statistics.AwayShotsOnTarget);
                    command.Parameters.AddWithValue("@HomePossession", statistics.HomePossession);
                    command.Parameters.AddWithValue("@AwayPossession", statistics.AwayPossession);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", statistics.ByUser);

                    await connection.OpenAsync();

                    bool result = false;

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = command;      
                        if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) result = true;
                        return result;
                    }
                }
            }
        }

        public async Task<IStatistics> GetStatisticsAsync(Guid MatchId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "select * from MatchStatistics where MatchId = @MatchId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@MatchId", MatchId);

                    await connection.OpenAsync();

                    IStatistics statistics = null;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (Convert.ToBoolean(reader["IsDeleted"]) != true)
                                {
                                    statistics = new Statistics()
                                    {
                                        Id = Guid.Parse(reader["Id"].ToString()),
                                        MatchId = Guid.Parse(reader["MatchID"].ToString()),
                                        HomeGoals = Convert.ToInt32(reader["HomeGoals"]),
                                        AwayGoals = Convert.ToInt32(reader["AwayGoals"]),
                                        HomeYellowCards = Convert.ToInt32(reader["HomeYellowCards"]),
                                        AwayYellowCards = Convert.ToInt32(reader["AwayYellowCards"]),
                                        HomeRedCards = Convert.ToInt32(reader["HomeRedCards"]),
                                        AwayRedCards = Convert.ToInt32(reader["AwayRedCards"]),
                                        HomeShots = Convert.ToInt32(reader["HomeShots"]),
                                        AwayShots = Convert.ToInt32(reader["AwayShots"]),
                                        HomeShotsOnTarget = Convert.ToInt32(reader["HomeShotsOnTarget"]),
                                        AwayShotsOnTarget = Convert.ToInt32(reader["AwayShotsOnTarget"]),
                                        HomePossession = Convert.ToInt32(reader["HomePossession"]),
                                        AwayPossession = Convert.ToInt32(reader["AwayPossession"]),
                                        HomeTeamSeasonId = Guid.Parse(reader["HomeTeamSeasonID"].ToString()),
                                        AwayTeamSeasonId = Guid.Parse(reader["AwayTeamSeasonID"].ToString())
                                    };
                                }
                            }
                        }
                        return statistics;
                    }
                }
            }
        }

        public async Task<List<IStatistics>> GetStatisticsByQueryAsync(StatisticsParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                List<IStatistics> statisticsList = new List<IStatistics>();
                string query = "select * from MatchStatistics where HomeTeamSeasonID = @TeamSeasonID or AwayTeamSeasonID = @TeamSeasonID";

                IQueryHelper<IStatistics, StatisticsParameters> queryHelper = new QueryHelper<IStatistics, StatisticsParameters>();

                //query += queryHelper.Sort.ApplySort(parameters.OrderBy + " desc");

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeamSeasonID", parameters.TeamSeasonID);
                    await connection.OpenAsync();
                                        
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (Convert.ToBoolean(reader["IsDeleted"]) != true)
                                {
                                    IStatistics statistics = new Statistics()
                                    {
                                        Id = Guid.Parse(reader["Id"].ToString()),
                                        MatchId = Guid.Parse(reader["MatchID"].ToString()),
                                        HomeTeamSeasonId = Guid.Parse(reader["HomeTeamSeasonID"].ToString()),
                                        AwayTeamSeasonId = Guid.Parse(reader["AwayTeamSeasonID"].ToString()),
                                        HomeGoals = Convert.ToInt32(reader["HomeGoals"]),
                                        AwayGoals = Convert.ToInt32(reader["AwayGoals"]),
                                        HomeYellowCards = Convert.ToInt32(reader["HomeYellowCards"]),
                                        AwayYellowCards = Convert.ToInt32(reader["AwayYellowCards"]),
                                        HomeRedCards = Convert.ToInt32(reader["HomeRedCards"]),
                                        AwayRedCards = Convert.ToInt32(reader["AwayRedCards"]),
                                        HomeShots = Convert.ToInt32(reader["HomeShots"]),
                                        AwayShots = Convert.ToInt32(reader["AwayShots"]),
                                        HomeShotsOnTarget = Convert.ToInt32(reader["HomeShotsOnTarget"]),
                                        AwayShotsOnTarget = Convert.ToInt32(reader["AwayShotsOnTarget"]),
                                        HomePossession = Convert.ToInt32(reader["HomePossession"]),
                                        AwayPossession = Convert.ToInt32(reader["AwayPossession"])                                        
                                    };

                                    statisticsList.Add(statistics);
                                }
                            }
                        }
                        return statisticsList;
                    }
                }
            }
        }

        public async Task<bool> UpdateStatisticsAsync(IStatistics statistics)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"update MatchStatistics set HomeTeamSeasonID = @HomeTeamSeasonID,
                                                   AwayTeamSeasonID = @AwayTeamSeasonID,
                                                   HomeGoals = @HomeGoals, 
                                                   AwayGoals = @AwayGoals, 
                                                   HomeYellowCards = @HomeYellowCards, 
                                                   AwayYellowCards = @AwayYellowCards, 
                                                   HomeRedCards = @HomeRedCards, 
                                                   AwayRedCards = @AwayRedCards, 
                                                   HomeShots = @HomeShots, 
                                                   AwayShots = @AwayShots, 
                                                   HomeShotsOnTarget = @HomeShotsOnTarget, 
                                                   AwayShotsOnTarget = @AwayShotsOnTarget, 
                                                   HomePossession = @HomePossession, 
                                                   AwayPossession = @AwayPossession,
                                                   ByUser = @ByUser
                                             where MatchID = @MatchId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HomeTeamSeasonID", statistics.HomeTeamSeasonId);
                    command.Parameters.AddWithValue("@AwayTeamSeasonID", statistics.AwayTeamSeasonId);
                    command.Parameters.AddWithValue("@MatchId", statistics.MatchId);
                    command.Parameters.AddWithValue("@HomeGoals", statistics.HomeGoals);
                    command.Parameters.AddWithValue("@AwayGoals", statistics.AwayGoals);
                    command.Parameters.AddWithValue("@HomeYellowCards", statistics.HomeYellowCards);
                    command.Parameters.AddWithValue("@AwayYellowCards", statistics.AwayYellowCards);
                    command.Parameters.AddWithValue("@HomeRedCards", statistics.HomeRedCards);
                    command.Parameters.AddWithValue("@AwayRedCards", statistics.AwayRedCards);
                    command.Parameters.AddWithValue("@HomeShots", statistics.HomeShots);
                    command.Parameters.AddWithValue("@AwayShots", statistics.AwayShots);
                    command.Parameters.AddWithValue("@HomeShotsOnTarget", statistics.HomeShotsOnTarget);
                    command.Parameters.AddWithValue("@AwayShotsOnTarget", statistics.AwayShotsOnTarget);
                    command.Parameters.AddWithValue("@HomePossession", statistics.HomePossession);
                    command.Parameters.AddWithValue("@AwayPossession", statistics.AwayPossession);
                    command.Parameters.AddWithValue("@ByUser", statistics.ByUser);

                    await connection.OpenAsync();

                    bool result = false;

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = command;
                        if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) result = true;
                    }
                    return result;
                }
            }
        }

        public async Task<bool> DeleteStatisticsAsync(Guid MatchId)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "update MatchStatistics set IsDeleted = @IsDeleted where MatchID = @MatchId";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@MatchId", MatchId);

                    await connection.OpenAsync();

                    bool result = false;

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = command;
                        if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) result = true;
                        return result;
                    }
                }
            }
        }
    }
}
