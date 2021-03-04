using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Results.Repository.Common;
using Results.Model.Common;
using System.Data.SqlClient;
using System.Data;
using Results.Model;
using Results.Common.Utils;
using Results.Common.Utils.QueryParameters;
using Results.Common.Utils.QueryHelpers;

namespace Results.Repository
{
    public class ClubRepository : RepositoryBase, IClubRepository
    {
        private SqlConnection _connection;
        private SqlCommand _command;

        public ClubRepository(SqlConnection connection) : base(connection)
        {
            _command = new SqlCommand(String.Empty, connection);
            _connection = connection;
            _connection.Open();
        }

        public ClubRepository(SqlTransaction transaction) : base(transaction.Connection)
        {
            _command = new SqlCommand(String.Empty, transaction.Connection, transaction);
        }
        public async Task<bool> CreateClubAsync(IClub club) 
        {
            
            _command.CommandText = @"INSERT INTO Club(StadiumID, Name, ClubAddress, ShortName, YearOfFoundation, Description, CreatedAt, UpdatedAt, IsDeleted, ByUser)
                            VALUES(@StadiumID, @Name, @ClubAddress, @ShortName, @YearOfFoundation, @Description, @CreatedAt, @UpdatedAt, @IsDeleted, @ByUser);";

                
            _command.Parameters.AddWithValue("@StadiumID", club.StadiumID);
            _command.Parameters.AddWithValue("@Name", club.Name);
            _command.Parameters.AddWithValue("@ClubAddress", club.ClubAddress);
            _command.Parameters.AddWithValue("@ShortName", club.ShortName);
            _command.Parameters.AddWithValue("@YearOfFoundation", club.YearOfFoundation);
            _command.Parameters.AddWithValue("@Description", club.Description);
            _command.Parameters.AddWithValue("@CreatedAt", club.CreatedAt = DateTime.Now);
            _command.Parameters.AddWithValue("@UpdatedAt", club.UpdatedAt = DateTime.Now);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = false;
            _command.Parameters.AddWithValue("@ByUser", club.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;
        }
        public async Task<bool> UpdateClubAsync(IClub club)
        {
            
            string query = @"UPDATE Club 
            SET Name = @Name, ClubAddress = @ClubAddress, ShortName = @ShortName, Description = @Description, UpdatedAt = @UpdatedAt, ByUser = @ByUser
            WHERE Id = @Id;";

                
            _command.Parameters.AddWithValue("@Id", club.Id);
            _command.Parameters.AddWithValue("@Name", club.Name);
            _command.Parameters.AddWithValue("@ClubAddress", club.ClubAddress);
            _command.Parameters.AddWithValue("@ShortName", club.ShortName);
            _command.Parameters.AddWithValue("@Description", club.Description);
            _command.Parameters.AddWithValue("@UpdatedAt", club.UpdatedAt = DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", club.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;

        }
        public async Task<bool> DeleteClubAsync(IClub club)
        {
            
            _command.CommandText = @"UPDATE Club
            SET IsDeleted = @IsDeleted, UpdatedAt = @UpdatedAt, ByUser = @ByUser
            WHERE Id = @Id;";

                
            _command.Parameters.AddWithValue("@Id", club.Id);
            _command.Parameters.Add("@IsDeleted", SqlDbType.Bit).Value = true;
            _command.Parameters.AddWithValue("@UpdatedAt", DateTime.Now);
            _command.Parameters.AddWithValue("@ByUser", club.ByUser);

            bool result = await _command.ExecuteNonQueryAsync() > 0;

            if (_command.Transaction == null)
            {
                _connection.Close();
            }

            return result;

        }
        public async Task<List<IClub>> GetAllClubsAsync()
        {
            
            _command.CommandText = "SELECT  Club.Id, Club.StadiumID, Club.Name, Club.ClubAddress, Club.ShortName, Club.YearOfFoundation, Club.Description, Stadium.Id, Stadium.Name, Stadium.StadiumAddress, Stadium.Capacity, Stadium.YearOfConstruction, Stadium.Description" +
                            " FROM Club LEFT JOIN Stadium ON (Club.StadiumID=Stadium.Id) WHERE Club.IsDeleted = @IsDeleted;";

            if(_command.Transaction != null)
            {
                _command.Parameters.Clear();
            }
            
            _command.Parameters.AddWithValue("@IsDeleted", false);

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                List<IClub> clubs = new List<IClub>();
                while (await reader.ReadAsync())
                {
                    IClub club = new Club()
                    {
                        Name = reader["Name"].ToString(),
                        ClubAddress = reader["ClubAddress"].ToString(),
                        ShortName = reader["ShortName"].ToString(),
                        YearOfFoundation = Convert.ToInt32(reader["YearOfFoundation"].ToString()),
                        Description = reader["Description"].ToString(),
                        Stadium = new Stadium()
                        {
                            Name = reader[8].ToString(),
                            StadiumAddress = reader[9].ToString(),
                            Capacity = int.Parse(reader[10].ToString()),
                            YearOfConstruction = Convert.ToInt32(reader[11].ToString()),
                            Description = reader[12].ToString(),
                        }
                };
                    clubs.Add(club);
                }
                return clubs;
            }
            
            
        }
        public async Task<IClub> GetClubByIdAsync(Guid id)
        {
            
            _command.CommandText = @"SELECT * FROM Club WHERE Id = @Id;";

            if(_command.Transaction != null)
            {
                _command.Parameters.Clear();
            }
            
            _command.Parameters.AddWithValue("@Id", id);

            
            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                if (await reader.ReadAsync())
                {
                    IClub club = new Club()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        StadiumID = Guid.Parse(reader["StadiumID"].ToString()),
                        Name = reader["Name"].ToString(),
                        ClubAddress = reader["ClubAddress"].ToString(),
                        ShortName = reader["ShortName"].ToString(),
                        YearOfFoundation = Convert.ToInt32(reader["YearOfFoundation"].ToString()),
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

                    return club;
                }

                if(_command.Transaction == null)
                {
                    _connection.Close();
                }

                return null;
            }
            
            
        }
        public async Task<PagedList<IClub>> GetClubsByQueryAsync(ClubParameters parameters)
        {
            IQueryHelper<IClub, ClubParameters> _queryHelper = GetQueryHelper<IClub, ClubParameters>();

            int totalCount = await GetTableCount<Club>();

            string query = @"SELECT * FROM Club ";

            query += _queryHelper.Filter.ApplyFilters(parameters);
            query += _queryHelper.Sort.ApplySort(parameters.OrderBy);
            query += _queryHelper.Paging.ApplayPaging(parameters.PageNumber, parameters.PageSize);

            _command.CommandText = query;

            using (SqlDataReader reader = await _command.ExecuteReaderAsync())
            {
                PagedList<IClub> clubList = new PagedList<IClub>(totalCount, parameters.PageNumber, parameters.PageSize);
                
                while (await reader.ReadAsync())
                {
                    IClub club = new Club()
                    {
                        Id = Guid.Parse(reader["Id"].ToString()),
                        StadiumID = Guid.Parse(reader["StadiumID"].ToString()),
                        Name = reader["Name"].ToString(),
                        ClubAddress = reader["ClubAddress"].ToString(),
                        ShortName = reader["ShortName"].ToString(),
                        YearOfFoundation = Convert.ToInt32(reader["YearOfFoundation"].ToString()),
                        Description = reader["Description"].ToString(),
                        IsDeleted = bool.Parse(reader["IsDeleted"].ToString()),
                        CreatedAt = DateTime.Parse(reader["CreatedAt"].ToString()),
                        UpdatedAt = DateTime.Parse(reader["UpdatedAt"].ToString()),
                        ByUser = Guid.Parse(reader["ByUser"].ToString())
                    };
                    clubList.Add(club);
                }
                reader.Close();

                if(_command.Transaction == null)
                {
                    _connection.Close();
                }

                return clubList;
            }
            
            
        }
    }
}
