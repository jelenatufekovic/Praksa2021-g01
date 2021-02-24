using Results.Repository.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using Results.Common.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model.Common;
using Results.Model;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils.QueryHelpers;

namespace Results.Repository
{
    public class LeagueRepository : ILeagueRepository
    {
        public async Task<bool> CreateLeagueAsync(ILeague league)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "insert into League(Name, ShortName, Rank, Country, CreatedAt, UpdatedAt, IsDeleted, ByUser) " +
                               "values(@Name, @ShortName, @Rank, @Country, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", league.Name);
                    command.Parameters.AddWithValue("@ShortName", league.ShortName);
                    command.Parameters.AddWithValue("@Rank", league.Rank);
                    command.Parameters.AddWithValue("@Country", league.Country);
                    command.Parameters.AddWithValue("@CreatedAt", league.CreatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", league.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", league.ByUser);

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

        public async Task<ILeague> GetLeagueByIdAsync(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "select * from League where Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);

                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        ILeague league = null;

                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                league = new League()
                                {
                                    Id = Guid.Parse(reader["Id"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    ShortName = reader["ShortName"].ToString(),
                                    Rank = Convert.ToInt32(reader["Rank"]),
                                    Country = reader["Country"].ToString()
                                };
                            }
                            return league;
                        }
                        return league;
                    }
                }
            }
        }

        public async Task<ILeague> GetLeagueByQueryAsync(LeagueParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "select * from League ";

                IQueryHelper<ILeague, LeagueParameters> queryHelper = new QueryHelper<ILeague, LeagueParameters>();

                query += queryHelper.Filter.ApplyFilters(parameters);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    ILeague league = null;
                    await connection.OpenAsync();

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {


                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                league = new League()
                                {
                                    Id = Guid.Parse(reader["Id"].ToString()),
                                    Name = reader["Name"].ToString(),
                                    ShortName = reader["ShortName"].ToString(),
                                    Rank = Convert.ToInt32(reader["Rank"]),
                                    Country = reader["Country"].ToString()
                                };
                            }
                            return league;
                        }
                    }
                    return league;
                }
            }
        }

        public async Task<bool> UpdateLeagueAsync(ILeague league)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "update League set Name = @Name, ShortName = @ShortName, Rank = @Rank, " +
                           "Country = @Country, UpdatedAt = @UpdatedAt, ByUser = @ByUser where Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", league.Id);
                    command.Parameters.AddWithValue("@Name", league.Name);
                    command.Parameters.AddWithValue("@ShortName", league.ShortName);
                    command.Parameters.AddWithValue("@Rank", league.Rank);
                    command.Parameters.AddWithValue("@Country", league.Country);
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", league.Id);

                    await connection.OpenAsync();

                    bool success = false;

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = command;
                        if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) success = true;

                        return success;
                    }
                }
            }
        }

        public async Task<bool> DeleteLeagueAsync(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "update League set IsDeleted = @IsDeleted where Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;

                    await connection.OpenAsync();

                    bool success = false;

                    using (SqlDataAdapter adapter = new SqlDataAdapter())
                    {
                        adapter.InsertCommand = command;
                        if (await adapter.InsertCommand.ExecuteNonQueryAsync() > 0) success = true;

                        return success;
                    }
                }
            }
        }
    }
}