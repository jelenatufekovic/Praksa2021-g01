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

            builder.RegisterType<FilterHelper<Player, PlayerParameters>>().As<IFilterHelper<Player, PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<PlayerParameters>>().As<ISortHelper<PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<PagingHelper>().As<IPagingHelper>().InstancePerDependency();

            builder.RegisterType<QueryHelper<Player, PlayerParameters>>().As<IQueryHelper<Player, PlayerParameters>>().InstancePerDependency();

        }
    }
}
