using Application.DTO;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.ComponentCQ.Data
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Component, ComponentDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status ? "Public" : "Private") );

            CreateMap<Commands.Create.Command, Component>();

            CreateMap<PropVM, Prop>();
            CreateMap<EventVM, Event>();
        }
    }
}
