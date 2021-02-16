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
        public async Task<List<ILeagueSeasonModel>> GetLeagueSeasonByIdAsync()
        {
            using (SqlConnection connection = new SqlConnection(@"Server=tcp:kruninserver.database.windows.net,1433;Initial Catalog=kruninabaza;Persist Security Info=False;User ID=krux031;Password=Desetisesti13;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
            //new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "SELECT * FROM LeagueSeason WHERE IsDeleted = @IsDeleted;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<ILeagueSeasonModel> list = new List<ILeagueSeasonModel>();
                        while (await reader.ReadAsync())
                        {
                            ILeagueSeasonModel model = new LeagueSeasonModel()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                LeagueID = Guid.Parse(reader["LeagueID"].ToString()),
                                SeasonID = Guid.Parse(reader["SeasonID"].ToString()),
                                Category = reader["Category"].ToString(),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                                //ByUser = Guid.Parse(reader["ByUser"].ToString()),
                            };
                            list.Add(model);
                        }
                        return list;
                    }
                }
            }
        }
    }
}
