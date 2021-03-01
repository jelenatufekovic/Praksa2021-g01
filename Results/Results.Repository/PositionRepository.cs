using Results.Model.Common;
using Results.Model;
using Results.Repository.Common;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils.QueryHelpers;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Results.Common.Utils;
using System.Data;

namespace Results.Repository
{
    public class PositionRepository : RepositoryBase, IPositionRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public PositionRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }
        public async Task<Guid> CreatePositionAsync(IPosition position) {
            _command.CommandText = @"DECLARE @PositionVar table(Id uniqueidentifier);
                                INSERT INTO Position (Name, ShortName, ByUser) 
                                    OUTPUT INSERTED.Id INTO @PositionVar
                                VALUES (@Name, @ShortName, @ByUser); 
                                    SELECT Id FROM @PositionVar;";


            _command.Parameters.Clear();

            _command.Parameters.AddWithValue("@Name", position.Name);
            _command.Parameters.AddWithValue("@ShortName", position.ShortName);
            _command.Parameters.AddWithValue("@ByUser", position.ByUser);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                await reader.ReadAsync();
                Guid result = Guid.Parse(reader["Id"].ToString());

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return result;
            }
        }

        public async Task<PagedList<IPosition>> GetPositionAsync(PositionParameters parameters) 
        {
            IQueryHelper<IPosition, PositionParameters> _queryHelper = GetQueryHelper<IPosition, PositionParameters>();

            int totalCount = await GetTableCount<Position>();

            string query = @"SELECT * FROM Position ";
            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<IPosition> positionList = new PagedList<IPosition>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    IPosition position = new Position()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        ShortName = reader["ShortName"].ToString(),
                        ByUser = Guid.Parse(reader["ByUser"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                    };
                    positionList.Add(position);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return positionList;
            }
        }

        public async Task<IPosition> GetPositionByIdAsync(Guid id)
        {
            _command.CommandText = "SELECT * FROM Position WHERE Id = @Id AND IsDeleted = @IsDeleted;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    IPosition position = new Position()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        Name = reader["Name"].ToString(),
                        ShortName = reader["ShortName"].ToString(),
                        ByUser = Guid.Parse(reader["ByUser"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                    };
                    reader.Close();

                    if (_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return position;
                }

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
            }
        }

        public async Task<bool> UpdatePositionAsync(IPosition position) {
            _command.CommandText = "UPDATE Position SET Name = @Name, ShortName = @ShortName, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", position.Id);
            _command.Parameters.AddWithValue("@Name", position.Name);
            _command.Parameters.AddWithValue("@ShortName", position.ShortName);
            _command.Parameters.AddWithValue("@ByUser", position.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeletePositionAsync(Guid id)
        {
            _command.CommandText = @"UPDATE Position SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
    }
}
