using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Repository.Common;
using Results.Model.Common;
using System.Data.SqlClient;
using Results.Repository;
using System.Data;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils.QueryHelpers;
using Results.Model;

namespace Results.Repository
{
    public class ScoreRepository : RepositoryBase, IScoreRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public ScoreRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public ScoreRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }

        public async Task<bool> CreateScoreAsync(IScore score)
        {
            _command.CommandText = "INSERT INTO Score (Id, MatchID, PlayerID, MatchMinute, Autogoal, ByUser" +
                                   "VALUES (@Id, @MathcID, @PlayerID, @MatchMinute, @Autogoal, @ByUser)";

            _command.Parameters.AddWithValue("@Id", score.Id);
            _command.Parameters.AddWithValue("@MatchID", score.MatchID);
            _command.Parameters.AddWithValue("@PlayerID", score.PlayerID);
            _command.Parameters.AddWithValue("@MatchMinute", score.MatchMinute);
            _command.Parameters.AddWithValue("@Autogoal", score.Autogoal);
            _command.Parameters.AddWithValue("@ByUser", score.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if(_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
        public async Task<bool> UpdateScoreAsync(IScore score)
        {
            _command.CommandText = "UPDATE Score " +
                "SET MatchMinute = @MatchMinute, Autogoal = @Autogoal, ByUser = @ByUser " +
                "WHERE Id = @Id, MatchID = @MatchID, PlayerID = @PlayerID";

            _command.Parameters.AddWithValue("@Id", score.Id);
            _command.Parameters.AddWithValue("@MatchID", score.MatchID);
            _command.Parameters.AddWithValue("@PlayerID", score.PlayerID);
            _command.Parameters.AddWithValue("@MatchMinute", score.MatchMinute);
            _command.Parameters.AddWithValue("@Autogoal", score.Autogoal);
            _command.Parameters.AddWithValue("@ByUser", score.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
        public async Task<bool> DeleteScoreAsync(Guid id, Guid byUser)
        {
            _command.CommandText = "UPDATE Score SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser WHERE Id = @Id;";

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

        public async Task<PagedList<IScore>> GetScoresByQueryAsync(ScoreParameters parameters)
        {
            IQueryHelper<IScore, ScoreParameters> _queryHelper = GetQueryHelper<IScore, ScoreParameters>();

            int totalCount = await GetTableCount<Score>();

            string query = @"SELECT * FROM Score ";

            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<IScore> scoreList = new PagedList<IScore>(totalCount, parameters.PageNumber, parameters.PageSize);

                while (await reader.ReadAsync())
                {
                    IScore score = new Score()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        MatchID = Guid.Parse(reader["MatchID"].ToString()),
                        PlayerID = Guid.Parse(reader["PlayerID"].ToString()),
                        MatchMinute = int.Parse(reader["YearOfConstruction"].ToString()),
                        Autogoal = bool.Parse(reader["Description"].ToString()),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString())
                    };
                    scoreList.Add(score);
                }
                reader.Close();

                if (_command.Transaction == null)
                {
                    _connection.Close();
                }

                return scoreList;
            }
        }
    }
}
