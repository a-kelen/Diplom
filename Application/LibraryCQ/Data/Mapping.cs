﻿using Application.DTO;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.LibraryCQ.Data
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Library, LibraryDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => !r.Status ? "Public" : "Private"))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .ForMember(d => d.ComponentsCount, o => o.MapFrom(r => r.Components.Count));

            CreateMap<Library, DetailedLibraryDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => !r.Status ? "Public" : "Private"))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .AfterMap<LikeAction>();

            CreateMap<Commands.Create.Command, Library>();
            
            CreateMap<ComponentVM, Component>();


        }
    }
}
