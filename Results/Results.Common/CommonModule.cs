using Autofac;
using Results.Common.Utils.QueryHelpers;
using Results.Common.Utils.QueryParameters;
using Results.Model;
using Results.Model.Common;

namespace Results.Common
{
    public class CommonModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<PagingHelper>().As<IPagingHelper>().InstancePerDependency();

            #region FilterHelpers

            builder.RegisterType<FilterHelper<IPlayer, PlayerParameters>>().As<IFilterHelper<IPlayer, PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<ICoach, CoachParameters>>().As<IFilterHelper<ICoach, CoachParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<IReferee, RefereeParameters>>().As<IFilterHelper<IReferee, RefereeParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<IStadium, StadiumParameters>>().As<IFilterHelper<IStadium, StadiumParameters>>().InstancePerDependency();

            #endregion

            #region SortHelpers

            builder.RegisterType<SortHelper<PlayerParameters>>().As<ISortHelper<PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<CoachParameters>>().As<ISortHelper<CoachParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<RefereeParameters>>().As<ISortHelper<RefereeParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<StadiumParameters>>().As<ISortHelper<StadiumParameters>>().InstancePerDependency();

            #endregion

            #region QueryHelpers

            builder.RegisterType<QueryHelper<IPlayer, PlayerParameters>>().As<IQueryHelper<IPlayer, PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<ICoach, CoachParameters>>().As<IQueryHelper<ICoach, CoachParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IReferee, RefereeParameters>>().As<IQueryHelper<IReferee, RefereeParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IStadium, StadiumParameters>>().As<IQueryHelper<IStadium, StadiumParameters>>().InstancePerDependency();

            #endregion
        }
    }
}
