using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Model.Common;
using Results.Repository.Common;
using Results.Model;
using System.Data;
using Results.Common.Utils.QueryHelpers;
using Results.Common.Utils.QueryParameters;
using Results.Repository;

namespace Results.Repository
{
    public class StadiumRepository : RepositoryBase, IStadiumRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public StadiumRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public StadiumRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }
        public async Task<bool> CreateStadiumAsync(IStadium stadium) 
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"INSERT INTO Stadium(Name, StadiumAddress, Capacity, YearOfConstruction, Description, CreatedAt, UpdatedAt, IsDeleted, ByUser)
                                VALUES(@Name, @StadiumAddress, @Capacity, @YearOfConstruction, @Description, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser);";
                
                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", stadium.Name);
                    command.Parameters.AddWithValue("@StadiumAddress", stadium.StadiumAddress);
                    command.Parameters.AddWithValue("@Capacity", stadium.Capacity);
                    command.Parameters.AddWithValue("@YearOfConstruction", stadium.YearOfConstruction);
                    command.Parameters.AddWithValue("@Description", stadium.Description);
                    command.Parameters.AddWithValue("@CreatedAt", stadium.CreatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@UpdatedAt", stadium.UpdatedAt = DateTime.Now);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
                    command.Parameters.AddWithValue("@ByUser", stadium.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
        public async Task<bool> UpdateStadiumAsync(IStadium stadium) 
        {
            using(SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"UPDATE Stadium 
                SET Name = @Name, StadiumAddress = @StadiumAddress, Capacity = @Capacity, Description = @Description, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                WHERE Id = @Id;";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", stadium.Id);
                    command.Parameters.AddWithValue("@Name", stadium.Name);
                    command.Parameters.AddWithValue("@StadiumAddress", stadium.StadiumAddress);
                    command.Parameters.AddWithValue("@Capacity", stadium.Capacity);
                    command.Parameters.AddWithValue("@Description", stadium.Description);
                    command.Parameters.AddWithValue("@UpdatedAt", stadium.UpdatedAt = DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", stadium.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
        public async Task<bool> DeleteStadiumAsync(IStadium stadium) 
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"UPDATE Stadium
                SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
                WHERE Id = @Id;";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", stadium.Id);
                    command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
                    command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
                    command.Parameters.AddWithValue("@ByUser", stadium.ByUser);

                    await connection.OpenAsync();
                    return (await command.ExecuteNonQueryAsync()) > 0;
                }
            }
        }
        public async Task<List<IStadium>> GetAllStadiumsAsync() 
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = "SELECT Name, StadiumAddress, Capacity, YearOfConstruction, Description" +
                               " FROM Stadium WHERE IsDeleted = @IsDeleted;";

                using(SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@IsDeleted", false);

                    await connection.OpenAsync();
                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<IStadium> stadiums = new List<IStadium>();
                        while(await reader.ReadAsync())
                        {
                            IStadium stadium = new Stadium()
                            {
                                Name = reader["Name"].ToString(),
                                StadiumAddress = reader["StadiumAddress"].ToString(),
                                Capacity = Convert.ToInt32(reader["Capacity"].ToString()),
                                YearOfConstruction = DateTime.Parse(reader["YearOfConstruction"].ToString()),
                                Description = reader["Description"].ToString()
                            };
                            stadiums.Add(stadium);
                        }
                        return stadiums;
                    }
                }
            }
        }
        public async Task<IStadium> GetStadiumByIdAsync(Guid id) 
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"SELECT * FROM Stadium WHERE Id = @Id;";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    await connection.OpenAsync();
                    using(SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if(await reader.ReadAsync())
                        {
                            return new Stadium()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                StadiumAddress = reader["StadiumAddress"].ToString(),
                                Capacity = int.Parse(reader["Capacity"].ToString()),
                                YearOfConstruction = DateTime.Parse(reader["YearOfConstruction"].ToString()),
                                Description = reader["Description"].ToString(),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                                ByUser = Guid.Parse(reader["ByUser"].ToString())
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<IStadium> GetStadiumByNameAsync(string name)
        {
            using (SqlConnection connection = new SqlConnection("data source=.; database=model; integrated security=SSPI"))
            {
                string query = @"SELECT * FROM Stadium WHERE Name = @name";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);

                    await connection.OpenAsync();
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            return new Stadium()
                            {
                                Id = Guid.Parse(reader["Id"].ToString()),
                                Name = reader["Name"].ToString(),
                                StadiumAddress = reader["StadiumAddress"].ToString(),
                                Capacity = int.Parse(reader["Capacity"].ToString()),
                                YearOfConstruction = DateTime.Parse(reader["YearOfConstruction"].ToString()),
                                Description = reader["Description"].ToString(),
                                IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                                CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                                UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                                ByUser = Guid.Parse(reader["ByUser"].ToString())
                            };
                        }
                        return null;
                    }
                }
            }
        }

        public async Task<PagedList<IStadium>> GetStadiumsByQueryAsync(StadiumParameters parameters)
        {
            IQueryHelper<IStadium, StadiumParameters> _queryHelper = GetQueryHelper<IStadium, StadiumParameters>();

            int totalCount = await GetTableCount<Stadium>();

            string query = @"SELECT * FROM Stadium ";

            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using(SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<IStadium> stadiumList = new PagedList<IStadium>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    IStadium stadium = new Stadium()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        StadiumAddress = reader["StadiumAddress"].ToString(),
                        Capacity = int.Parse(reader["Capacity"].ToString()),
                        YearOfConstruction = DateTime.Parse(reader["YearOfConstruction"].ToString()),
                        Description = reader["Description"].ToString(),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString())
                    };
                    stadiumList.Add(stadium);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return stadiumList;
            }
        }
    }
}
