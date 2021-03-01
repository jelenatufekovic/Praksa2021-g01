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
            builder.RegisterType<FilterHelper<IClub, ClubParameters>>().As<IFilterHelper<IClub, ClubParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<IScore, ScoreParameters>>().As<IFilterHelper<IScore, ScoreParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<ICard, CardParameters>>().As<IFilterHelper<ICard, CardParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<ISubstitution, SubstitutionParameters>>().As<IFilterHelper<ISubstitution, SubstitutionParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<IMatch, MatchQueryParameters>>().As<IFilterHelper<IMatch, MatchQueryParameters>>().InstancePerDependency();
            builder.RegisterType<FilterHelper<IPosition, PositionParameters>>().As<IFilterHelper<IPosition, PositionParameters>>().InstancePerDependency();

            #endregion

            #region SortHelpers

            builder.RegisterType<SortHelper<PlayerParameters>>().As<ISortHelper<PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<CoachParameters>>().As<ISortHelper<CoachParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<RefereeParameters>>().As<ISortHelper<RefereeParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<StadiumParameters>>().As<ISortHelper<StadiumParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<ClubParameters>>().As<ISortHelper<ClubParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<ScoreParameters>>().As<ISortHelper<ScoreParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<CardParameters>>().As<ISortHelper<CardParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<SubstitutionParameters>>().As<ISortHelper<SubstitutionParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<MatchQueryParameters>>().As<ISortHelper<MatchQueryParameters>>().InstancePerDependency();
            builder.RegisterType<SortHelper<PositionParameters>>().As<ISortHelper<PositionParameters>>().InstancePerDependency();

            #endregion

            #region QueryHelpers

            builder.RegisterType<QueryHelper<IPlayer, PlayerParameters>>().As<IQueryHelper<IPlayer, PlayerParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<ICoach, CoachParameters>>().As<IQueryHelper<ICoach, CoachParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IReferee, RefereeParameters>>().As<IQueryHelper<IReferee, RefereeParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IStadium, StadiumParameters>>().As<IQueryHelper<IStadium, StadiumParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IClub, ClubParameters>>().As<IQueryHelper<IClub, ClubParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IScore, ScoreParameters>>().As<IQueryHelper<IScore, ScoreParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<ICard, CardParameters>>().As<IQueryHelper<ICard, CardParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<ISubstitution, SubstitutionParameters>>().As<IQueryHelper<ISubstitution, SubstitutionParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IMatch, MatchQueryParameters>>().As<IQueryHelper<IMatch, MatchQueryParameters>>().InstancePerDependency();
            builder.RegisterType<QueryHelper<IPosition, PositionParameters>>().As<IQueryHelper<IPosition, PositionParameters>>().InstancePerDependency();

            #endregion
        }
    }
}
