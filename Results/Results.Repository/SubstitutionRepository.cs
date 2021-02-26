using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Common.Utils;
using Results.Common.Utils.QueryHelpers;
using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;
using Results.Repository.Common;

namespace Results.Repository
{
    public class SubstitutionRepository : RepositoryBase, ISubstitutionRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public SubstitutionRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public SubstitutionRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<bool> CreateSubstitutionAsync(ISubstitution substitution)
        {
            _command.CommandText = "INSERT INTO Substitution (Id, MatchID, PlayerInID, PlayerOutID, MatchMinute, ByUser" +
                                   "VALUES (@Id, @MathcID, @PlayerInID, @PlayerOutID, @MatchMinute, @ByUser)";

            _command.Parameters.AddWithValue("@Id", substitution.Id);
            _command.Parameters.AddWithValue("@MatchID", substitution.MatchID);
            _command.Parameters.AddWithValue("@PlayerInID", substitution.PlayerInID);
            _command.Parameters.AddWithValue("@PlayerOutID", substitution.PlayerOutID);
            _command.Parameters.AddWithValue("@MatchMinute", substitution.MatchMinute);
            _command.Parameters.AddWithValue("@ByUser", substitution.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> UpdateSubstitutionAsync(ISubstitution substitution)
        {
            _command.CommandText = "UPDATE Substitution " +
                "SET PlayerInID = @PlayerInID, PlayerOutID = @PlayerOutID, MatchMinute = @MatchMinute, ByUser = @ByUser " +
                "WHERE Id = @Id, MatchID = @MatchID";

            _command.Parameters.AddWithValue("@Id", substitution.Id);
            _command.Parameters.AddWithValue("@MatchID", substitution.MatchID);
            _command.Parameters.AddWithValue("@PlayerInID", substitution.PlayerInID);
            _command.Parameters.AddWithValue("@PlayerOutID", substitution.PlayerOutID);
            _command.Parameters.AddWithValue("@MatchMinute", substitution.MatchMinute);
            _command.Parameters.AddWithValue("@ByUser", substitution.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<bool> DeleteSubstitutionAsync(Guid id, Guid byUser)
        {
            _command.CommandText = "UPDATE Substitution SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

            _command.Parameters.AddWithValue("@Id", id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", byUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }

        public async Task<PagedList<ISubstitution>> GetSubstitutionsByQueryAsync(SubstitutionParameters parameters)
        {
            IQueryHelper<ISubstitution, SubstitutionParameters> _queryHelper = GetQueryHelper<ISubstitution, SubstitutionParameters>();

            int totalCount = await GetTableCount<Substitution>();

            string query = @"SELECT * FROM Substitution ";

            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<ISubstitution> substitutionList = new PagedList<ISubstitution>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    ISubstitution substitution = new Substitution()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        MatchID = Guid.Parse(reader["MatchID"].ToString()),
                        PlayerInID = Guid.Parse(reader["PlayerID"].ToString()),
                        PlayerOutID = Guid.Parse(reader["PlayerID"].ToString()),
                        MatchMinute = int.Parse(reader["MatchMinute"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString())
                    };
                    substitutionList.Add(substitution);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return substitutionList;
            }
        }
    }
}
