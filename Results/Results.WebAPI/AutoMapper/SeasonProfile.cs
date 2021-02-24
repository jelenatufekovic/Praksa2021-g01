using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Season;

namespace Results.WebAPI.AutoMapper
{
    public class SeasonProfile : Profile
    {
        #region Constructors

        public SeasonProfile()
        {
            CreateMap<CreateSeasonRest, ISeason>();
            CreateMap<ISeason, Models.RestModels.Season.SeasonViewModel>();
            CreateMap<UpdateSeasonRest, ISeason>();
        }

        #endregion Constructors
    }
}