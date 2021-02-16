using AutoMapper;
using Results.Model;
using Results.WebAPI.Models.RestModels.User;
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
            CreateMap<LeagueSeasonModel, LeagueSeasonViewModel>();
        }
    }
}