﻿using Autofac;
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
            base.Load(builder);

            builder.RegisterType<ResultsService>().As<IResultsService>().InstancePerDependency();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerDependency();
            builder.RegisterType<PersonService>().As<IPersonService>().InstancePerDependency();
            builder.RegisterType<PlayerService>().As<IPlayerService>().InstancePerDependency();
            builder.RegisterType<CoachService>().As<ICoachService>().InstancePerDependency();
            builder.RegisterType<RefereeService>().As<IRefereeService>().InstancePerDependency();
        }
    }
}
