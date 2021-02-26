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

        public async Task<PagedList<ICard>> GetCardsByQueryAsync(CardParameters parameters)
        {
            IQueryHelper<ICard, CardParameters> _queryHelper = GetQueryHelper<ICard, CardParameters>();

            int totalCount = await GetTableCount<Card>();

            string query = @"SELECT * FROM Card ";

            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<ICard> cardList = new PagedList<ICard>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    ICard card = new Card()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        MatchID = Guid.Parse(reader["MatchID"].ToString()),
                        PlayerID = Guid.Parse(reader["PlayerID"].ToString()),
                        YellowCard = bool.Parse(reader["YellowCard"].ToString()),
                        RedCard = bool.Parse(reader["RedCard"].ToString()),
                        MatchMinute = int.Parse(reader["MatchMinute"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString())
                    };
                    cardList.Add(card);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return cardList;
            }
        }
    }
}
