using Autofac;
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
        }
    }
}
