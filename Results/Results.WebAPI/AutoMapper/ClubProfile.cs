using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Club;

namespace Results.WebAPI.AutoMapper
{
    public class ClubProfile : Profile
    {
        public ClubProfile()
        {
            CreateMap<CreateClubRest, IClub>();
            CreateMap<UpdateClubRest, IClub>();
            CreateMap<DeleteClubRest, IClub>();
            CreateMap<IClub, GetAllClubsRest>();
        }
    }
}