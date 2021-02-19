using Results.Model.Common;
using Results.Repository.Common;
using Results.Model;
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
    public class StandingsRepository : IStandingsRepository
    {
        public async Task<List<IStandings>> GetTableByLeagueSeasonAsync(Guid guid)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"SELECT Club.Id as ClubID, Club.Name as Club, Played, Won, Draw, Lost, GoalsScored, GoalsConceded, Points 
                                FROM Standing
                                JOIN Club ON Standing.ClubID = Club.Id
                                WHERE LeagueSeasonID = @leagueSeasonID
                                and Standing.IsDeleted = @IsDeleted
                                Order By Points DESC;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@leagueSeasonID", guid);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<IStandings> list = new List<IStandings>();
                        while (await reader.ReadAsync())
                        {
                            IStandings model = new Standings()
                            {
                                ClubID = Guid.Parse(reader["ClubID"].ToString()),
                                ClubName = reader["Club"].ToString(),
                                Played = Convert.ToInt32(reader["Played"].ToString()), 
                                Won = Convert.ToInt32(reader["Won"].ToString()),
                                Draw = Convert.ToInt32(reader["Draw"].ToString()),
                                Lost = Convert.ToInt32(reader["Lost"].ToString()),
                                GoalsScored = Convert.ToInt32(reader["GoalsScored"].ToString()),
                                GoalsConceded = Convert.ToInt32(reader["GoalsConceded"].ToString()),
                                Points = Convert.ToInt32(reader["Points"].ToString()),
                            };
                            list.Add(model);
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<string> CheckExistingClubForLeagueSeasonAsync(IStandings standings)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"SELECT LeagueSeasonID, ClubID, IsDeleted FROM Standing
                                WHERE LeagueSeasonID = @LeagueSeasonID
                                AND ClubID = @ClubID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@leagueSeasonID", standings.LeagueSeasonID);
                    command.Parameters.AddWithValue("@ClubID", standings.ClubID);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        IStandings model = null;
                        while (await reader.ReadAsync())
                        {
                            model =new Standings
                            {
                                ClubID = Guid.Parse(reader["ClubID"].ToString()),
                                LeagueSeasonID = Guid.Parse(reader["LeagueSeasonID"].ToString()),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString())
                            };
                        }

                        if (model == null) return "NoExist";

                        if (model.IsDeleted == true) return "Deleted";

                        return "Exist";
                    }
                }
            }
        }

        public async Task<bool> UpdateTableFromDelete(IStandings standings)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE Standing
                                SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                                WHERE LeagueSeasonID = @LeagueSeasonID
                                AND ClubID = @ClubID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeagueSeasonID", standings.LeagueSeasonID);
                    command.Parameters.AddWithValue("@ClubID", standings.ClubID);
                    command.Parameters.AddWithValue("@UpdatedAt", standings.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", standings.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<bool> CreateTableByLeagueSeasonAsync(IStandings standings)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString())) 
            {
                string query = @"INSERT INTO Standing(LeagueSeasonID, ClubID, Played, Won, Draw, Lost, GoalsScored, GoalsConceded, Points, CreatedAt, UpdatedAt, IsDeleted, ByUser)
                                VALUES(@LeagueSeasonID, @ClubID, @Played, @Won, @Draw, @Lost, @GoalsScored, @GoalsConceded, @Points, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeagueSeasonID", standings.LeagueSeasonID);
                    command.Parameters.AddWithValue("@ClubID", standings.ClubID);
                    command.Parameters.AddWithValue("@Played", standings.Played = 0);
                    command.Parameters.AddWithValue("@Won", standings.Won = 0);
                    command.Parameters.AddWithValue("@Draw", standings.Draw = 0);
                    command.Parameters.AddWithValue("@Lost", standings.Lost = 0);
                    command.Parameters.AddWithValue("@GoalsScored", standings.GoalsScored = 0);
                    command.Parameters.AddWithValue("@GoalsConceded", standings.GoalsConceded = 0);
                    command.Parameters.AddWithValue("@Points", standings.Points = 0);
                    command.Parameters.AddWithValue("@CreatedAt", standings.CreatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", standings.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", standings.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<bool> UpdateTableForClubAsync(IStandings standings)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString())) 
            {
                string query = @"UPDATE Standing
                                SET LeagueSeasonID = @LeagueSeasonID, ClubID=@ClubID, Played=@Played, Won=@Won, Draw=@Draw, Lost=@Lost, GoalsScored=@GoalsScored, GoalsConceded=@GoalsConceded, Points=@Points, UpdatedAt=@UpdatedAt, IsDeleted=@IsDeleted, ByUser=@ByUser
                                WHERE LeagueSeasonID = @LeagueSeasonID
                                AND ClubID = @ClubID";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeagueSeasonID", standings.LeagueSeasonID);
                    command.Parameters.AddWithValue("@ClubID", standings.ClubID);
                    command.Parameters.AddWithValue("@Played", (standings.Won + standings.Draw + standings.Lost));
                    command.Parameters.AddWithValue("@Won", standings.Won);
                    command.Parameters.AddWithValue("@Draw", standings.Draw);
                    command.Parameters.AddWithValue("@Lost", standings.Lost);
                    command.Parameters.AddWithValue("@GoalsScored", standings.GoalsScored);
                    command.Parameters.AddWithValue("@GoalsConceded", standings.GoalsConceded);
                    command.Parameters.AddWithValue("@Points", ((standings.Won*3)+standings.Draw));
                    command.Parameters.AddWithValue("@UpdatedAt", standings.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", standings.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<bool> DeleteTableByLeagueSeasonAsync(IStandings standings)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString())) 
            {
                string query = @"UPDATE Standing
                                SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                                WHERE LeagueSeasonID = @LeagueSeasonID;";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeagueSeasonID", standings.LeagueSeasonID);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", standings.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }

        public async Task<bool> DeleteClubTableByLeagueSeasonAsync(IStandings standings)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"UPDATE Standing
                                SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                                WHERE LeagueSeasonID = @LeagueSeasonID
                                AND ClubID = @ClubID";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeagueSeasonID", standings.LeagueSeasonID);
                    command.Parameters.AddWithValue("@ClubID", standings.ClubID);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", standings.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
    }
}
