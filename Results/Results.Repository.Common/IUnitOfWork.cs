using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository.Common
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository User { get; }
        IPersonRepository Person { get; }
        IPlayerRepository Player { get; }
        ICoachRepository Coach { get; }
        IRefereeRepository Referee { get; }
        IStadiumRepository Stadium { get; }
        IStatisticsRepository Statistics { get; }
        ITeamSeasonRepository TeamSeason { get; }
        ITeamRegistrationRepository TeamRegistration { get; }

        void Commit();
    }
}
