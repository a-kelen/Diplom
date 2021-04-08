﻿using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.UserCQ.Data
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, UserDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Firstname + " " + s.Lastname))
                .ForMember(x => x.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.Email))
                .AfterMap<TokenAction>();

            CreateMap<User, DetailedUserDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Firstname + " " + s.Lastname))
                .ForMember(x => x.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(x => x.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .AfterMap<FollowedAction>();
        }
    }
}
