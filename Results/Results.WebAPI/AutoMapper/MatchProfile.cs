using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Common.Utils.QueryParameters;
using Results.WebAPI.Models.RestModels.Match;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.AutoMapper
{
    public class MatchProfile : Profile
    {
        public MatchProfile()
        {
            CreateMap<IMatch, MatchViewModel>();
            CreateMap<UpdateMatchRest, IMatch>();
            CreateMap<MatchIdProviderRest, IMatch>();
            CreateMap<MatchIdProviderRest, MatchParameters>();
        }
    }
}