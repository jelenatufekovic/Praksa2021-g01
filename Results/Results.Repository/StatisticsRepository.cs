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
        private SqlConnection _connection;
        private SqlCommand _command;

        public StatisticsRepository(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public StatisticsRepository(SqlTransaction transaction)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<bool> CreateStatisticsAsync(IStatistics statistics)
        {
            _command.CommandText = @"insert into MatchStatistics (MatchId, 
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

            _command.Parameters.AddWithValue("@MatchId", statistics.MatchId);
            _command.Parameters.AddWithValue("@HomeGoals", statistics.HomeGoals);
            _command.Parameters.AddWithValue("@AwayGoals", statistics.AwayGoals);
            _command.Parameters.AddWithValue("@HomeYellowCards", statistics.HomeYellowCards);
            _command.Parameters.AddWithValue("@AwayYellowCards", statistics.AwayYellowCards);
            _command.Parameters.AddWithValue("@HomeRedCards", statistics.HomeRedCards);
            _command.Parameters.AddWithValue("@AwayRedCards", statistics.AwayRedCards);
            _command.Parameters.AddWithValue("@HomeShots", statistics.HomeShots);
            _command.Parameters.AddWithValue("@AwayShots", statistics.AwayShots);
            _command.Parameters.AddWithValue("@HomeShotsOnTarget", statistics.HomeShotsOnTarget);
            _command.Parameters.AddWithValue("@AwayShotsOnTarget", statistics.AwayShotsOnTarget);
            _command.Parameters.AddWithValue("@HomePossession", statistics.HomePossession);
            _command.Parameters.AddWithValue("@AwayPossession", statistics.AwayPossession);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
            _command.Parameters.AddWithValue("@ByUser", statistics.ByUser);

            bool result = false;

            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.InsertCommand = _command;
                if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) result = true;

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return result;
            }
        }

        public async Task<IStatistics> GetStatisticsAsync(Guid MatchId)
        {
            _command.CommandText = "select * from MatchStatistics where MatchId = @MatchId";

            _command.Parameters.AddWithValue("@MatchId", MatchId);

            IStatistics statistics = null;

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
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
                                AwayPossession = Convert.ToInt32(reader["AwayPossession"])
                            };
                        }
                    }
                }

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return statistics;
            }
        }

        public async Task<bool> UpdateStatisticsAsync(IStatistics statistics)
        {

            _command.CommandText = @"update MatchStatistics set 
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

            _command.Parameters.AddWithValue("@MatchId", statistics.MatchId);
            _command.Parameters.AddWithValue("@HomeGoals", statistics.HomeGoals);
            _command.Parameters.AddWithValue("@AwayGoals", statistics.AwayGoals);
            _command.Parameters.AddWithValue("@HomeYellowCards", statistics.HomeYellowCards);
            _command.Parameters.AddWithValue("@AwayYellowCards", statistics.AwayYellowCards);
            _command.Parameters.AddWithValue("@HomeRedCards", statistics.HomeRedCards);
            _command.Parameters.AddWithValue("@AwayRedCards", statistics.AwayRedCards);
            _command.Parameters.AddWithValue("@HomeShots", statistics.HomeShots);
            _command.Parameters.AddWithValue("@AwayShots", statistics.AwayShots);
            _command.Parameters.AddWithValue("@HomeShotsOnTarget", statistics.HomeShotsOnTarget);
            _command.Parameters.AddWithValue("@AwayShotsOnTarget", statistics.AwayShotsOnTarget);
            _command.Parameters.AddWithValue("@HomePossession", statistics.HomePossession);
            _command.Parameters.AddWithValue("@AwayPossession", statistics.AwayPossession);
            _command.Parameters.AddWithValue("@ByUser", statistics.ByUser);

            bool result = false;

            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.InsertCommand = _command;
                if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) result = true;
            }

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeleteStatisticsAsync(Guid MatchId)
        {

            _command.CommandText = "update MatchStatistics set IsDeleted = @IsDeleted where MatchID = @MatchId";

            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@MatchId", MatchId);

            bool result = false;

            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                adapter.InsertCommand = _command;
                if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) result = true;

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return result;
            }
        }


        public async Task<List<Guid>> GetClubIDsAsync(Guid MatchId)
        {
            List<Guid> clubIDs = null;
            string query = @"select t.clubID from TeamSeason t left join Match m on t.Id = m.HomeTeamSeasonID where m.Id = @MatchId;
                                 select t.clubID from TeamSeason t left join Match m on t.Id = m.AwayTeamSeasonID where m.Id = @MatchId;";
            
            _command.Parameters.AddWithValue("@MatchId", MatchId);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                clubIDs = new List<Guid>();

                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        clubIDs.Add(Guid.Parse(reader["ClubID"].ToString()));
                    }

                    if (reader.NextResult())
                    {
                        while (await reader.ReadAsync())
                        {
                            clubIDs.Add(Guid.Parse(reader["ClubID"].ToString()));
                        }
                    }
                }
            }
            
            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return clubIDs;
        }
    }
}
