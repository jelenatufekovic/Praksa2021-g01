using AutoMapper;
using Results.Model;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Player;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.AutoMapper
{
    public class PlayerProfile : Profile
    {
        public PlayerProfile()
        {
            CreateMap<PlayerRest, Player>();
            CreateMap<PlayerRest, IPlayer>();
            CreateMap<IPlayer, PlayerViewModel>();
        }
    }
}