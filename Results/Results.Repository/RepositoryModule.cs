using Autofac;
using Results.Common.Utils;
using Results.Common.Utils.QueryHelpers;
using Results.Common.Utils.QueryParameters;
using Results.Model.Common;
using Results.Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Repository
{
    public class RepositoryModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<ResultsRepository>().As<IResultsRepository>().InstancePerDependency();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerDependency();
            builder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerDependency();
            builder.RegisterType<PlayerRepository>().As<IPlayerRepository>().InstancePerDependency();
            builder.RegisterType<CoachRepository>().As<ICoachRepository>().InstancePerDependency();
            builder.RegisterType<RefereeRepository>().As<IRefereeRepository>().InstancePerDependency();

            builder.RegisterType<RepositoryFactory>().As<IRepositoryFactory>().InstancePerDependency();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerDependency();

            builder.RegisterType<FilterHelper<IPlayer, PlayerParameters>>().As<IFilterHelper<IPlayer, PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<PlayerParameters>>().As<ISortHelper<PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<PagingHelper>().As<IPagingHelper>().InstancePerDependency();

            builder.RegisterType<MatchRepository>().As<IMatchRepository>().InstancePerDependency();
            builder.RegisterType<LeagueSeasonRepository>().As<ILeagueSeasonRepository>().InstancePerDependency();
            builder.RegisterType<StandingsRepository>().As<IStandingsRepository>().InstancePerDependency();

            builder.RegisterType<StadiumRepository>().As<IStadiumRepository>().InstancePerDependency();
            builder.RegisterType<ClubRepository>().As<IClubRepository>().InstancePerDependency();

            builder.RegisterType<SeasonRepository>().As<ISeasonRepository>().InstancePerDependency();
            builder.RegisterType<LeagueRepository>().As<ILeagueRepository>().InstancePerDependency();
            builder.RegisterType<ScoreRepository>().As<IScoreRepository>().InstancePerDependency();
            builder.RegisterType<CardRepository>().As<ICardRepository>().InstancePerDependency();
            builder.RegisterType<SubstitutionRepository>().As<ISubstitutionRepository>().InstancePerDependency();
            builder.RegisterType<StatisticsRepository>().As<IStatisticsRepository>().InstancePerDependency();
            builder.RegisterType<TeamRegistrationRepository>().As<ITeamRegistrationRepository>().InstancePerDependency();
            builder.RegisterType<TeamSeasonRepository>().As<ITeamSeasonRepository>().InstancePerDependency();
            builder.RegisterType<PositionRepository>().As<IPositionRepository>().InstancePerDependency();
        }
    }
}
