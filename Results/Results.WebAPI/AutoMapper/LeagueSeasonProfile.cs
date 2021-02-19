using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.LeagueSeason;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.AutoMapper
{
    public class LeagueSeasonProfile : Profile
    {
        public LeagueSeasonProfile()
        {
            CreateMap<LeagueSeason, LeagueSeasonViewModel>();
            CreateMap<LeagueSeasonIdProviderRest, ILeagueSeason>();
            CreateMap<CreateLeagueSeasonRest, ILeagueSeason>();
            CreateMap<CreateLeagueSeasonRest, LeagueSeason>();
        }
    }
}