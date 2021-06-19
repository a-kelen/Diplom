﻿using Application.DTO;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.LibraryCQ.Data
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Library, LibraryDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status ? "Public" : "Private"))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .ForMember(d => d.HasAvatar, o => o.MapFrom(r => r.Avatar != null))
                .ForMember(d => d.ComponentsCount, o => o.MapFrom(r => r.Components.Count))
                .ForMember(d => d.Labels, o => o.MapFrom(r => r.Labels.Select(x => x.Name)))
                .AfterMap<LibraryItemLikeAction>();

            CreateMap<Library, DetailedLibraryDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status ? "Public" : "Private"))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .ForMember(d => d.HasAvatar, o => o.MapFrom(r => r.Avatar != null))
                .ForMember(d => d.Labels, o => o.MapFrom(r => r.Labels.Select(x => x.Name)))
                .AfterMap<LibraryLikeAction>();

            CreateMap<Commands.Create.Command, Library>()
                .ForMember(x => x.Labels, o => o.Ignore());
            
            CreateMap<ComponentVM, Component>();

            CreateMap<Library, ActivityDTO>()
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .ForMember(d => d.HasAvatar, o => o.MapFrom(r => r.Owner.Avatar != null))
                .ForMember(d => d.Date, o => o.MapFrom(r => r.Created))
                .ForMember(d => d.Name, o => o.MapFrom(r => r.Name))
                .ForMember(d => d.Type, o => o.MapFrom(r => "library"));

        }
    }
}
