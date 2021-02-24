using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.League;

namespace Results.WebAPI.AutoMapper
{
    public class LeagueProfile : Profile
    {
        #region Constructors

        public LeagueProfile()
        {
            CreateMap<CreateLeagueRest, ILeague>();
            CreateMap<ILeague, Models.RestModels.League.LeagueViewModel>();
            CreateMap<UpdateLeagueRest, ILeague>();
        }

        #endregion Constructors
    }
}