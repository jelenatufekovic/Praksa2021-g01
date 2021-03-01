using Results.Common.Utils;
using Results.Repository.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private SqlConnection _connection;
        private SqlTransaction _transaction;

        private IUserRepository _user;
        private IPersonRepository _person;
        private IPlayerRepository _player;
        private ICoachRepository _coach;
        private IRefereeRepository _referee;
        private IStadiumRepository _stadium;
        private IClubRepository _club;
        private IScoreRepository _score;
        private ICardRepository _card;
        private IStatisticsRepository _statistics;
        private ITeamSeasonRepository _teamSeason;
        private ITeamRegistrationRepository _teamRegistration;

        public UnitOfWork(SqlConnection connection)
        {
            _connection = connection;
            _connection.Open();
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(Transaction);
                }
                return _user;
            }
        }

        public IPersonRepository Person
        {
            get
            {
                if (_person == null)
                {
                    _person = new PersonRepository(Transaction);
                }
                return _person;
            }
        }

        public IPlayerRepository Player
        {
            get
            {
                if (_player == null)
                {
                    _player = new PlayerRepository(Transaction);
                }
                return _player;
            }
        }

        public ICoachRepository Coach
        {
            get
            {
                if (_coach == null)
                {
                    _coach = new CoachRepository(Transaction);
                }
                return _coach;
            }
        }

        public IRefereeRepository Referee
        {
            get
            {
                if (_referee == null)
                {
                    _referee = new RefereeRepository(Transaction);
                }
                return _referee;
            }
        }

        public IStadiumRepository Stadium
        {
            get
            {
                if (_stadium == null)
                {
                    _stadium = new StadiumRepository(Transaction);
                }
                return _stadium;
            }
        }

        public IClubRepository Club
        {
            get
            {
                if (_club == null)
                {
                    _club = new ClubRepository(Transaction);
                }
                return _club;
            }
        }

        public IScoreRepository Score
        {
            get
            {
                if (_score == null)
                {
                    _score = new ScoreRepository(Transaction);
                }
                return _score;
            }
        }

        public ICardRepository Card
        {
            get
            {
                if (_card == null)
                {
                    _card = new CardRepository(Transaction);
                }
                return _card;
            }
        }
        public IStatisticsRepository Statistics
        {
            get
            {
                if (_statistics == null)
                {
                    _statistics = new StatisticsRepository(Transaction);
                }
                return _statistics;
            }
        }

        public ITeamRegistrationRepository TeamRegistration
        {
            get
            {
                if (_teamRegistration == null)
                {
                    _teamRegistration = new TeamRegistrationRepository(Transaction);
                }
                return _teamRegistration;
            }
        }

        public ITeamSeasonRepository TeamSeason
        {
            get
            {
                if (_teamSeason == null)
                {
                    _teamSeason = new TeamSeasonRepository(Transaction);
                }
                return _teamSeason;
            }
        }

        public void Commit()
        {
            if (_transaction == null)
            {
                throw new InvalidOperationException("Transaction have already been committed. Check your transaction handling.");
            }

            _transaction.Commit();
            _transaction = null;
        }

        public void Dispose()
        {
            if (_transaction != null)
            {
                _transaction.Rollback();
                _transaction = null;
            }

            if (_connection != null)
            {
                _connection.Close();
                _connection = null;
            }
        }

        private SqlTransaction Transaction
        {
            get
            {
                if (_transaction == null)
                {
                    _transaction = _connection.BeginTransaction();
                }
                return _transaction;
            }
        }
    }
}
