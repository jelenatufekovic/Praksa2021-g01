using Autofac;
using Results.Service.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Service
{
    public class ServiceModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ResultsService>().As<IResultsService>().InstancePerDependency();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<PlayerService>().As<IPlayerService>().InstancePerDependency();
            builder.RegisterType<CoachService>().As<ICoachService>().InstancePerDependency();
            builder.RegisterType<RefereeService>().As<IRefereeService>().InstancePerDependency();
            builder.RegisterType<ResultsService>().As<IResultsService>().InstancePerDependency();

            builder.RegisterType<MatchService>().As<IMatchService>().InstancePerDependency();
            builder.RegisterType<LeagueSeasonService>().As<ILeagueSeasonService>().InstancePerDependency();
            builder.RegisterType<StandingsService>().As<IStandingsService>().InstancePerDependency();

            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerDependency();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerDependency();
            builder.RegisterType<StadiumService>().As<IStadiumService>().InstancePerDependency();
            builder.RegisterType<ClubService>().As<IClubService>().InstancePerDependency();
            builder.RegisterType<ScoreService>().As<IScoreService>().InstancePerDependency();
            builder.RegisterType<CardService>().As<ICardService>().InstancePerDependency();
            builder.RegisterType<SubstitutionService>().As<ISubstitutionService>().InstancePerDependency();

            builder.RegisterType<SeasonService>().As<ISeasonService>().InstancePerDependency();
            builder.RegisterType<LeagueService>().As<ILeagueService>().InstancePerDependency();
            builder.RegisterType<StatisticsService>().As<IStatisticsService>().InstancePerDependency();
            builder.RegisterType<PositionService>().As<IPositionService>().InstancePerDependency();
            builder.RegisterType<TeamSeasonService>().As<ITeamSeasonService>().InstancePerDependency();

            base.Load(builder);


            builder.RegisterType<TokenService>().As<ITokenService>().InstancePerDependency();
            builder.RegisterType<UserManager>().As<IUserManager>().InstancePerDependency();
        }
    }
}
