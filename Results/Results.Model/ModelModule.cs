using Autofac;
using Results.Model.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Results.Model
{
    public class ModelModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType<ResultsModel>().As<IResultsModel>().InstancePerDependency();
            builder.RegisterType<ModelBase>().As<IModelBase>().InstancePerDependency();
            builder.RegisterType<User>().As<IUser>().InstancePerDependency();
            builder.RegisterType<Person>().As<IPerson>().InstancePerDependency();
            builder.RegisterType<Player>().As<IPlayer>().InstancePerDependency();
            builder.RegisterType<Coach>().As<ICoach>().InstancePerDependency();
            builder.RegisterType<Referee>().As<IReferee>().InstancePerDependency();
            builder.RegisterType<Person>().As<IPerson>().InstancePerDependency();
            builder.RegisterType<Player>().As<IPlayer>().InstancePerDependency();
            builder.RegisterType<Coach>().As<ICoach>().InstancePerDependency();
            builder.RegisterType<Referee>().As<IReferee>().InstancePerDependency();
            builder.RegisterType<LeagueSeason>().As<ILeagueSeason>().InstancePerDependency();
            builder.RegisterType<Standings>().As<IStandings>().InstancePerDependency();
            builder.RegisterType<Stadium>().As<IStadium>().InstancePerDependency();
            builder.RegisterType<Club>().As<IClub>().InstancePerDependency();
            builder.RegisterType<Score>().As<IScore>().InstancePerDependency();
            builder.RegisterType<Season>().As<ISeason>().InstancePerDependency();
            builder.RegisterType<League>().As<ILeague>().InstancePerDependency();
            builder.RegisterType<Statistics>().As<IStatistics>().InstancePerDependency();
            builder.RegisterType<Match>().As<IMatch>().InstancePerDependency();
            builder.RegisterType<Position>().As<IPosition>().InstancePerDependency();
            builder.RegisterType<TeamSeason>().As<ITeamSeason>().InstancePerDependency();
            builder.RegisterType<TeamRegistration>().As<ITeamRegistration>().InstancePerDependency();


            builder.RegisterType<Card>().As<ICard>().InstancePerDependency();
            builder.RegisterType<Substitution>().As<ISubstitution>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
