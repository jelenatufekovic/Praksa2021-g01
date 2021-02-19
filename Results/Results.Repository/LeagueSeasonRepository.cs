using Results.Model.Common;
using Results.Repository.Common;
using Results.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Results.Common.Utils;
using System.Data.SqlClient;

namespace Results.Repository
{
    public class LeagueSeasonRepository : ILeagueSeasonRepository
    {
        public async Task<List<ILeagueSeason>> GetAllLeagueSeasonIdAsync()
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "SELECT * FROM LeagueSeason WHERE IsDeleted = @IsDeleted;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<ILeagueSeason> list = new List<ILeagueSeason>();
                        while (await reader.ReadAsync())
                        {
                            ILeagueSeason model = new LeagueSeason()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                LeagueID = Guid.Parse(reader["LeagueID"].ToString()),
                                SeasonID = Guid.Parse(reader["SeasonID"].ToString()),
                                Category = reader["Category"].ToString(),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                                ByUser = Guid.Parse(reader["ByUser"].ToString()),
                            };
                            list.Add(model);
                        }
                        return list;
                    }
                }
            }
        }

        public async Task<Guid> LeagueSeasonRegistrationAsync(ILeagueSeason leagueSeasonModel)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = @"DECLARE @LeagueSeasonVar table(Id uniqueidentifier);
                            INSERT INTO LeagueSeason (LeagueID, SeasonID, Category, CreatedAt, UpdatedAt, IsDeleted, ByUser) 
                            OUTPUT INSERTED.Id INTO @LeagueSeasonVar
                            VALUES (@LeagueID, @SeasonID, @Category, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser); 
                            SELECT Id FROM @LeagueSeasonVar;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@LeagueID", leagueSeasonModel.LeagueID);
                    command.Parameters.AddWithValue("@SeasonID", leagueSeasonModel.SeasonID);
                    command.Parameters.AddWithValue("@Category", leagueSeasonModel.Category);
                    command.Parameters.AddWithValue("@CreatedAt", leagueSeasonModel.CreatedAt);
                    command.Parameters.AddWithValue("@UpdatedAt", leagueSeasonModel.UpdatedAt);
                    command.Parameters.AddWithValue("@IsDeleted", leagueSeasonModel.IsDeleted);
                    command.Parameters.AddWithValue("@ByUser", leagueSeasonModel.ByUser);


                    await connection.OpenAsync();
                    using (SqlDataReader result = await command.ExecuteReaderAsync())
                    {
                        await result.ReadAsync();
                        Guid guid = Guid.Parse(result["Id"].ToString());

                        result.Close();
                        return guid;
                    }
                }
            }
        }
    }
}
