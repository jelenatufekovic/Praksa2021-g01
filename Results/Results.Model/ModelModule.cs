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
            builder.RegisterType<User>().As<IUser>().InstancePerDependency();
            builder.RegisterType<LeagueSeasonModel>().As<ILeagueSeasonModel>().InstancePerDependency();
            builder.RegisterType<StandingsModel>().As<IStandingsModel>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
