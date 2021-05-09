using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.UserCQ.Data
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<User, CurrentUserDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Firstname + " " + s.Lastname))
                .ForMember(x => x.Username, o => o.MapFrom(s => s.UserName))
                .AfterMap<TokenAction>();

            CreateMap<User, UserDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Firstname + " " + s.Lastname))
                .ForMember(x => x.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(x => x.Email, o => o.MapFrom(s => s.Email))
                .ForMember(d => d.HasAvatar, o => o.MapFrom(r => r.Avatar != null))
                .ForMember(x => x.FollowerCount, o => o.MapFrom(s => s.Followers.Count))
                .ForMember(x => x.LibraryCount, o => o.MapFrom(s => s.Libraries.Count()))
                .ForMember(x => x.ComponentCount, o => o.MapFrom(s => s.Components.Count()));

            CreateMap<User, DetailedUserDTO>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Firstname + " " + s.Lastname))
                .ForMember(x => x.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.HasAvatar, o => o.MapFrom(r => r.Avatar != null))
                .ForMember(x => x.FollowersCount, o => o.MapFrom(s => s.Followers.Count))
                .AfterMap<FollowedAction>();

            CreateMap<User, FollowDTO>()
                .ForMember(x => x.Username, o => o.MapFrom(s => s.UserName))
                .ForMember(d => d.HasAvatar, o => o.MapFrom(r => r.Avatar != null))
                .ForMember(x => x.Name, o => o.MapFrom(s => s.Firstname + " " + s.Lastname));
        }
    }
}
