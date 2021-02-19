using Autofac;
using Results.Common.Utils;
using Results.Model;
using Results.Model.Common;

namespace Results.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<FilterHelper<IPlayer, PlayerParameters>>().As<IFilterHelper<IPlayer, PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<PlayerParameters>>().As<ISortHelper<PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<PagingHelper>().As<IPagingHelper>().InstancePerDependency();

            builder.RegisterType<QueryHelper<IPlayer, PlayerParameters>>().As<IQueryHelper<IPlayer, PlayerParameters>>().InstancePerDependency();

        }
    }
}
