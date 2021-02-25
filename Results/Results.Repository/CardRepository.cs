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
    public class CardRepository : RepositoryBase, ICardRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;
        
        public CardRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public CardRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<bool> CreateCardAsync(ICard card)
        {
            _command.CommandText = "INSERT INTO Card (Id, MatchID, PlayerID, YellowCard, RedCard, MatchMinute" +
                                   "VALUES (@Id, @MathcID, @PlayerID, @YellowCard, @RedCard, @MatchMinute)";

            _command.Parameters.AddWithValue("@Id", card.Id);
            _command.Parameters.AddWithValue("@MatchID", card.MatchID);
            _command.Parameters.AddWithValue("@PlayerID", card.PlayerID);
            _command.Parameters.AddWithValue("@YellowCard", card.YellowCard);
            _command.Parameters.AddWithValue("@RedCard", card.RedCard);
            _command.Parameters.AddWithValue("@MatchMinute", card.MatchMinute);
            

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
        public async Task<bool> UpdateCardAsync(ICard card)
        {
            _command.CommandText = "UPDATE Card " +
                "SET YellowCard = @YellowCard, RedCard = @RedCard, MatchMinute = @MatchMinute " +
                "WHERE Id = @Id, MatchID = @MatchID, PlayerID = @PlayerID";

            _command.Parameters.AddWithValue("@Id", card.Id);
            _command.Parameters.AddWithValue("@MatchID", card.MatchID);
            _command.Parameters.AddWithValue("@PlayerID", card.PlayerID);
            _command.Parameters.AddWithValue("@YellowCard", card.YellowCard);
            _command.Parameters.AddWithValue("@RedCard", card.RedCard);
            _command.Parameters.AddWithValue("@MatchMinute", card.MatchMinute);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
        public async Task<bool> DeleteCardAsync(Guid id, Guid byUser)
        {
            _command.CommandText = "UPDATE Card SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

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
