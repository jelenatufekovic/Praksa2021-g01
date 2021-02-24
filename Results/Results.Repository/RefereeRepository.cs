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
    public class RefereeRepository : IRefereeRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public RefereeRepository(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public RefereeRepository(SqlTransaction transaction)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<bool> CreateRefereeAsync(IReferee referee)
        {
            _command.CommandText = @"INSERT INTO Referee (Id, Rating, ByUser) VALUES (@Id, @Rating, @ByUser)";

            _command.Parameters.AddWithValue("@Id", referee.Id);
            _command.Parameters.AddWithValue("@Rating", referee.Rating);
            _command.Parameters.AddWithValue("@ByUser", referee.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeleteRefereeAsync(Guid id, Guid ByUser)
        {
            _command.CommandText = "UPDATE Referee SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<IReferee> GetRefereeByIdAsync(Guid id)
        {
            _command.CommandText = @"SELECT
                                    Referee.Id AS Id,
                                    Person.FirstName AS FirstName,
                                    Person.LastName AS LastName,
                                    Person.Country AS Country,
                                    Person.DateOfBirth AS DateOfBirth,
                                    Referee.Rating AS Rating,
                                    Referee.ByUser AS ByUser,
                                    Referee.IsDeleted AS IsDeleted,
                                    Referee.CreatedAt AS CreatedAt,
                                    Referee.UpdatedAt AS UpdatedAt
                                FROM Referee 
                                LEFT JOIN Person ON Referee.Id = Person.Id
                                WHERE Referee.Id = @Id;";

            _command.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    IReferee referee = new Referee()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Country = reader["Country"].ToString(),
                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                        Rating = Int32.Parse(reader["Rating"].ToString()),
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

                    return referee;
                }

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
            }
        }

        public async Task<PagedList<IReferee>> FindRefereesAsync(RefereeParameters parameters)
        {
            IQueryHelper<IReferee, RefereeParameters> _queryHelper = new QueryHelper<IReferee, RefereeParameters>();

            int totalCount = await GetTableCount();

            string query = @"SELECT 
                                Referee.Id AS Id,
                                Person.FirstName AS FirstName,
                                Person.LastName AS LastName,
                                Person.Country AS Country,
                                Person.DateOfBirth AS DateOfBirth,
                                Referee.Rating AS Rating,
                                Referee.ByUser AS ByUser,
                                Referee.IsDeleted AS IsDeleted,
                                Referee.CreatedAt AS CreatedAt,
                                Referee.UpdatedAt AS UpdatedAt
                            FROM Referee 
                            LEFT JOIN Person ON Referee.Id = Person.Id ";
            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<IReferee> refereeList = new PagedList<IReferee>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    IReferee referee = new Referee()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Country = reader["Country"].ToString(),
                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                        Rating = Int32.Parse(reader["Rating"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                    };
                    refereeList.Add(referee);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return refereeList;
            }
        }

        public async Task<bool> UpdateRefereeAsync(IReferee referee)
        {
            _command.CommandText = "UPDATE Referee SET Rating = @Rating, @UpdatedAt = UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", referee.Id);
            _command.Parameters.AddWithValue("@Rating", referee.Rating);
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", referee.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        private async Task<int> GetTableCount()
        {
            _command.CommandText = "SELECT COUNT(*) AS TotalCount FROM Referee;";
            return (Int32)(await _command.ExecuteScalarAsync());
        }
    }
}
