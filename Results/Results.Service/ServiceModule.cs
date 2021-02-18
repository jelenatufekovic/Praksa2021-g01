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
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<ResultsService>().As<IResultsService>().InstancePerDependency();
            builder.RegisterType<LeagueSeasonService>().As<ILeagueSeasonService>().InstancePerDependency();
            builder.RegisterType<StandingsService>().As<IStandingsService>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
