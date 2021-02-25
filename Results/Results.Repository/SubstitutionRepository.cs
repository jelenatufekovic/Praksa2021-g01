using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
