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
            base.Load(builder);

            builder.RegisterType<ResultsModel>().As<IResultsModel>().InstancePerDependency();
            builder.RegisterType<User>().As<IUser>().InstancePerDependency();
            builder.RegisterType<Person>().As<IPerson>().InstancePerDependency();
            builder.RegisterType<Player>().As<IPlayer>().InstancePerDependency();
            builder.RegisterType<Coach>().As<ICoach>().InstancePerDependency();
            builder.RegisterType<Referee>().As<IReferee>().InstancePerDependency();
            builder.RegisterType<LeagueSeason>().As<ILeagueSeason>().InstancePerDependency();
            builder.RegisterType<Standings>().As<IStandings>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
