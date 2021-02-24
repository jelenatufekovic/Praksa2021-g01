using Results.Model.Common;
using Results.Repository.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using Results.Common.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Model;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils.QueryHelpers;

namespace Results.Repository
{
    public class SeasonRepository : ISeasonRepository
    {
        public async Task<bool> CreateSeasonAsync(ISeason season)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "insert into Season (Name, Period, YearOfStart, CreatedAt, UpdatedAt, IsDeleted, ByUser)" +
                               "values (@Name, @Period, @YearOfStart, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser)";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", season.Name);
                    command.Parameters.AddWithValue("@Period", season.Period);
                    command.Parameters.AddWithValue("@YearOfStart", season.YearOfStart);
                    command.Parameters.AddWithValue("@CreatedAt", season.CreatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", season.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", season.ByUser);

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

        public async Task<bool> DeleteSeasonAsync(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "update Season set IsDeleted = @IsDeleted where Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@Id", Id);

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

        public async Task<ISeason> GetSeasonByIdAsync(Guid Id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "select * from Season where Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", Id);

                    await connection.OpenAsync();

                    ISeason season = null;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (Convert.ToBoolean(reader["IsDeleted"]) != true)
                                {
                                    season = new Season()
                                    {
                                        Id = Guid.Parse(reader["Id"].ToString()),
                                        Name = reader["Name"].ToString(),
                                        Period = reader["Period"].ToString(),
                                        YearOfStart = Convert.ToInt32(reader["YearOfStart"])
                                    };
                                }
                            }
                        }
                        return season;
                    }
                }
            }
        }

        public async Task<ISeason> GetSeasonByQueryAsync(SeasonParameters parameters)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                IQueryHelper<ISeason, SeasonParameters> queryHelper = new QueryHelper<ISeason, SeasonParameters>();

                string query = "select * from Season ";
                query += queryHelper.Filter.ApplyFilters(parameters);

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    await connection.OpenAsync();

                    ISeason season = null;

                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (reader.HasRows)
                        {
                            while (await reader.ReadAsync())
                            {
                                if (Convert.ToBoolean(reader["IsDeleted"]) != true)
                                {
                                    season = new Season()
                                    {
                                        Id = Guid.Parse(reader["Id"].ToString()),
                                        Name = reader["Name"].ToString(),
                                        Period = reader["Period"].ToString(),
                                        YearOfStart = Convert.ToInt32(reader["YearOfStart"])
                                    };
                                }
                            }
                        }
                        return season;
                    }
                }
            }
        }

        public async Task<bool> UpdateSeasonAsync(ISeason season)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                string query = "update Season set Name = @Name, Period = @Period, YearOfStart = @YearOfStart, " +
                               "UpdatedAt = @UpdatedAt, ByUser = @ByUser where Id = @Id";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", season.Name);
                    command.Parameters.AddWithValue("@Period", season.Period);
                    command.Parameters.AddWithValue("@YearOfStart", season.YearOfStart);
                    command.Parameters.AddWithValue("@UpdatedAt", season.UpdatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", season.ByUser);
                    command.Parameters.AddWithValue("@Id", season.Id);

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
    }
}