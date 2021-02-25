using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using Results.WebAPI.Models.RestModels.Standing;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.AutoMapper
{
    public class StandingsProfile : Profile
    {
        public StandingsProfile()
        {
            CreateMap<IStandings, StandingsViewModel>();
            CreateMap<StandingsIdProviderRest, IStandings>();
            CreateMap<UpdateStandingsRest, IStandings>();
            CreateMap<StandingsIdProviderRest, StandingsParameters>();
        }
    }
}