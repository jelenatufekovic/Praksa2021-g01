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
            builder.RegisterType<ResultsRepository>().As<IResultsRepository>().InstancePerDependency();
            base.Load(builder);
        }
    }
}
