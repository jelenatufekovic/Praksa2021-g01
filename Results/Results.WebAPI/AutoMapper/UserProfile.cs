﻿using AutoMapper;
using Results.Model;
using Results.WebAPI.Models.RestModels.User;
using Results.WebAPI.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Results.WebAPI.AutoMapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserRest, User>();
            CreateMap<UpdateUserRest, User>();
            CreateMap<UpdateUserPasswordRest, User>();
            CreateMap<User, UserViewModel>();
        }
    }
}