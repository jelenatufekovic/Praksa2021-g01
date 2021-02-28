using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.Common.Utils;
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
            CreateMap<PagedList<IMatch>, MatchViewModel>();
            CreateMap<IMatch, MatchViewModel>();
            CreateMap<UpdateMatchRest, IMatch>();
            CreateMap<CreateMatchRest, IMatch>();
            CreateMap<CreateMatchRest, MatchParameters>();
            CreateMap<QueryMatchRest, MatchQueryParameters>();
        }
    }
}