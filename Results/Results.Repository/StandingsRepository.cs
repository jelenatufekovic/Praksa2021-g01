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
        public async Task<List<IStandingsModel>> GetTableByLeagueSeasonAsync(Guid guid)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=tcp:kruninserver.database.windows.net,1433;Initial Catalog=kruninabaza;Persist Security Info=False;User ID=krux031;Password=Desetisesti13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            //new SqlConnection(ConnectionString.GetDefaultConnectionString()))
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
                        List<IStandingsModel> list = new List<IStandingsModel>();
                        while (await reader.ReadAsync())
                        {
                            IStandingsModel model = new StandingsModel()
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
                                //ByUser = Guid.Parse(reader["ByUser"].ToString()),
                            };
                            list.Add(model);
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<bool> CheckExistingClubForLeagueSeasonAsync(IStandingsModel standings)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=tcp:kruninserver.database.windows.net,1433;Initial Catalog=kruninabaza;Persist Security Info=False;User ID=krux031;Password=Desetisesti13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            //new SqlConnection(ConnectionString.GetDefaultConnectionString())) PROMIJENI U STANDINGS QUERY
            {
                string query = @"SELECT LeagueSeasonID, ClubID FROM Standing
                                WHERE LeagueSeasonID = @LeagueSeasonID
                                AND ClubID = @ClubID
                                AND IsDeleted = @IsDeleted";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@leagueSeasonID", standings.LeagueSeasonID);
                    command.Parameters.AddWithValue("@ClubID", standings.ClubID);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        IStandingsModel model = null;
                        while (await reader.ReadAsync())
                        {
                            model =new StandingsModel
                            {
                                ClubID = Guid.Parse(reader["ClubID"].ToString()),
                                LeagueSeasonID = Guid.Parse(reader["LeagueSeasonID"].ToString())
                            };
                        }

                        if(model == null)
                        {
                            return false;
                        }
                        return true;
                    }
                }
            }
        }

        public async Task<bool> CreateTableByLeagueSeasonAsync(IStandingsModel standings)
        {
            using (SqlConnection connection = new SqlConnection(@"Server=tcp:kruninserver.database.windows.net,1433;Initial Catalog=kruninabaza;Persist Security Info=False;User ID=krux031;Password=Desetisesti13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            //new SqlConnection(ConnectionString.GetDefaultConnectionString())) PROMIJENI U STANDINGS QUERY
            {
                string query = @"INSERT INTO Standing(LeagueSeasonID, ClubID, Played, Won, Draw, Lost, GoalsScored, GoalsConceded, Points, CreatedAt, UpdatedAt, IsDeleted, ByUser)
                                VALUES(@LeagueSeasonID, @ClubID, @Played, @Won, @Draw, @Lost, @GoalsScored, @GoalsConceded, @Points, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@leagueSeasonID", standings.LeagueSeasonID);
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
                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = command;

                        try
                        {
                            if(await adapter.InsertCommand.ExecuteNonQueryAsync() == 0)
                            {
                                return false;
                            }
                        }

                        catch (DbException)
                        {
                            return false;
                        }
                        finally
                        {
                            connection.Close();
                        }

                        return true;
                    }
                }
            }
        }
    }
}
