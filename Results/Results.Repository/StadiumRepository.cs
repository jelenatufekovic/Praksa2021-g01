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
            
            _command.CommandText = @"INSERT INTO Stadium(Name, StadiumAddress, Capacity, YearOfConstruction, Description, CreatedAt, UpdatedAt, IsDeleted, ByUser)
                                    VALUES(@Name, @StadiumAddress, @Capacity, @YearOfConstruction, @Description, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser);";
                
                
            _command.Parameters.AddWithValue("@Name", stadium.Name);
            _command.Parameters.AddWithValue("@StadiumAddress", stadium.StadiumAddress);
            _command.Parameters.AddWithValue("@Capacity", stadium.Capacity);
            _command.Parameters.AddWithValue("@YearOfConstruction", stadium.YearOfConstruction);
            _command.Parameters.AddWithValue("@Description", stadium.Description);
            _command.Parameters.AddWithValue("@CreatedAt", stadium.CreatedAt = DateTime.Now);
            _command.Parameters.AddWithValue("@UpdatedAt", stadium.UpdatedAt = DateTime.Now);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
            _command.Parameters.AddWithValue("@ByUser", stadium.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if(_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
        public async Task<bool> UpdateStadiumAsync(IStadium stadium) 
        {
            
            _command.CommandText = @"UPDATE Stadium 
            SET Name = @Name, StadiumAddress = @StadiumAddress, Capacity = @Capacity, Description = @Description, UpdatedAt = @UpdatedAt, ByUser = @ByUser
            WHERE Id = @Id;";

                
            _command.Parameters.AddWithValue("@Id", stadium.Id);
            _command.Parameters.AddWithValue("@Name", stadium.Name);
            _command.Parameters.AddWithValue("@StadiumAddress", stadium.StadiumAddress);
            _command.Parameters.AddWithValue("@Capacity", stadium.Capacity);
            _command.Parameters.AddWithValue("@Description", stadium.Description);
            _command.Parameters.AddWithValue("@UpdatedAt", stadium.UpdatedAt = DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", stadium.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;
                
            if(_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;            
        }
        public async Task<bool> DeleteStadiumAsync(IStadium stadium) 
        {
            
            _command.CommandText = @"UPDATE Stadium
            SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
            WHERE Id = @Id;";

                
            _command.Parameters.AddWithValue("@Id", stadium.Id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", stadium.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;
                
            if(_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
        public async Task<List<IStadium>> GetAllStadiumsAsync() 
        {
            
            _command.CommandText = "SELECT Id, Name, StadiumAddress, Capacity, YearOfConstruction, Description" +
                            " FROM Stadium WHERE IsDeleted = @IsDeleted;";

                
            if(_command.Transaction != null)
            {
                _command.Parameters.Clear();
            }

            _command.Parameters.AddWithValue("@IsDeleted", false);

            using(SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                List<IStadium> stadiums = new List<IStadium>();
                while(await reader.ReadAsync())
                {
                    IStadium stadium = new Stadium()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        StadiumAddress = reader["StadiumAddress"].ToString(),
                        Capacity = Convert.ToInt32(reader["Capacity"].ToString()),
                        YearOfConstruction = Convert.ToInt32(reader["YearOfConstruction"].ToString()),
                        Description = reader["Description"].ToString()
                    };
                    stadiums.Add(stadium);
                }
                return stadiums;
            }
                
            
        }
        public async Task<IStadium> GetStadiumByIdAsync(Guid id) 
        {
            
            _command.CommandText = @"SELECT * FROM Stadium WHERE Id = @Id;";

            if(_command.Transaction != null)
            {
                _command.Parameters.Clear();
            }
            
            _command.Parameters.AddWithValue("@Id", id);

            using(SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if(await reader.ReadAsync())
                {
                    IStadium stadium = new Stadium()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        StadiumAddress = reader["StadiumAddress"].ToString(),
                        Capacity = int.Parse(reader["Capacity"].ToString()),
                        YearOfConstruction = Convert.ToInt32(reader["YearOfConstruction"].ToString()),
                        Description = reader["Description"].ToString(),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString())
                    };

                    reader.Close();

                    if(_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return stadium;
                }

                if(_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
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
                        YearOfConstruction = Convert.ToInt32(reader["YearOfConstruction"].ToString()),
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
