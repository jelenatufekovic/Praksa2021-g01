using Results.Common.Utils;
using Results.Common.Utils.QueryHelpers;
using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;
using Results.Repository.Common;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class CoachRepository : ICoachRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public CoachRepository(SqlConnection connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public CoachRepository(SqlTransaction transaction)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<bool> CreateCoachAsync(ICoach coach)
        {
            _command.CommandText = "INSERT INTO Coach (Id, CoachType, ByUser) VALUES (@Id, @CoachType, @ByUser)";

            _command.Parameters.AddWithValue("@Id", coach.Id);
            _command.Parameters.AddWithValue("@CoachType", coach.CoachType);
            _command.Parameters.AddWithValue("@ByUser", coach.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeleteCoachAsync(Guid id, Guid ByUser)
        {
            _command.CommandText = "UPDATE Coach SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

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

        public async Task<ICoach> GetCoachByIdAsync(Guid id)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString.GetDefaultConnectionString()))
            {
                _command.CommandText = @"SELECT
                                    Coach.Id AS Id,
                                    Person.FirstName AS FirstName,
                                    Person.LastName AS LastName,
                                    Person.Country AS Country,
                                    Person.DateOfBirth AS DateOfBirth,
                                    Coach.CoachType AS CoachType,
                                    Coach.ByUser AS ByUser,
                                    Coach.IsDeleted AS IsDeleted,
                                    Coach.CreatedAt AS CreatedAt,
                                    Coach.UpdatedAt AS UpdatedAt
                                FROM Coach 
                                LEFT JOIN Person ON Coach.Id = Person.Id
                                WHERE Coach.Id = @Id;";

                _command.Parameters.AddWithValue("@Id", id);
                using (SqlDataReader reader = await _command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        ICoach coach = new Coach()
                        {
                            Id = Guid.Parse(reader["Id"].ToString()),
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Country = reader["Country"].ToString(),
                            DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                            CoachType = reader["CoachType"].ToString(),
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

                        return coach;
                    }

                    if (_command.Transaction == null)
                    {
                        _connection.Close();
                    }

                    return null;
                }
            }
        }

        public async Task<PagedList<ICoach>> FindCoachesAsync(CoachParameters parameters)
        {
            IQueryHelper<ICoach, CoachParameters> _queryHelper = new QueryHelper<ICoach, CoachParameters>();

            int totalCount = await GetTableCount();

            string query = @"SELECT 
                                Coach.Id AS Id,
                                Person.FirstName AS FirstName,
                                Person.LastName AS LastName,
                                Person.Country AS Country,
                                Person.DateOfBirth AS DateOfBirth,
                                Coach.CoachType AS CoachType,
                                Coach.ByUser AS ByUser,
                                Coach.IsDeleted AS IsDeleted,
                                Coach.CreatedAt AS CreatedAt,
                                Coach.UpdatedAt AS UpdatedAt
                             FROM Coach 
                             LEFT JOIN Person ON Coach.Id = Person.Id ";
            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<ICoach> coachList = new PagedList<ICoach>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    ICoach coach = new Coach()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        FirstName = reader["FirstName"].ToString(),
                        LastName = reader["LastName"].ToString(),
                        Country = reader["Country"].ToString(),
                        DateOfBirth = DateTime.Parse(reader["DateOfBirth"].ToString()),
                        CoachType = reader["CoachType"].ToString(),
                        ByUser = Guid.Parse(reader["ByUser"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                    };
                    coachList.Add(coach);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return coachList;
            }
        }

        public async Task<bool> UpdateCoachAsync(ICoach coach)
        {
            _command.CommandText = "UPDATE Coach SET CoachType = @CoachType, @UpdatedAt = UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", coach.Id);
            _command.Parameters.AddWithValue("@CoachType", coach.CoachType);
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", coach.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        private async Task<int> GetTableCount()
        {
            _command.CommandText = "SELECT COUNT(*) AS TotalCount FROM Coach;";
            return (Int32)(await _command.ExecuteScalarAsync());
        }
    }
}
