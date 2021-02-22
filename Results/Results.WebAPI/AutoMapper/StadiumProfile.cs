using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using Results.Model.Common;
using Results.WebAPI.Models.RestModels.Stadium;

namespace Results.WebAPI.AutoMapper
{
    public class StadiumProfile : Profile
    {
        public StadiumProfile()
        {
            CreateMap<CreateStadiumRest, IStadium>();
            CreateMap<UpdateStadiumRest, IStadium>();
            CreateMap<DeleteStadiumRest, IStadium>();
            CreateMap<IStadium, GetAllStadiumsRest>();
        }
    }
}