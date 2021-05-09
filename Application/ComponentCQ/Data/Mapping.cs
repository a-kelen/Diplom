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
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status ? "Public" : "Private"))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .AfterMap<ComponentItemLikeAction>();

            CreateMap<Component, DetailedComponentDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status ? "Public" : "Private"))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName != null ? r.Owner.UserName : r.Library.Owner.UserName))
                .ForMember(d => d.Library, o => o.MapFrom(r => r.Library))
                .ForMember(d => d.Events, o => o.MapFrom(r => r.Events))
                .ForMember(d => d.Props, o => o.MapFrom(r => r.Props))
                .ForMember(d => d.Slots, o => o.MapFrom(r => r.Slots))
                .ForMember(d => d.Description, o => o.MapFrom(r => r.Description ?? ""))
                .ForMember(d => d.Dependencies, o => o.MapFrom(r => r.Dependencies ?? ""))
                .AfterMap<ComponentLikeAction>();

            CreateMap<Commands.Create.Command, Component>();

            CreateMap<PropVM, Prop>();
            CreateMap<EventVM, Event>();
            CreateMap<SlotVM, Slot>();

            CreateMap<Prop, PropDTO>();
            CreateMap<Event, EventDTO>();
            CreateMap<Slot, SlotDTO>();

            CreateMap<Component, ActivityDTO>()
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .ForMember(d => d.HasAvatar, o => o.MapFrom(r => r.Owner.Avatar != null))
                .ForMember(d => d.Date, o => o.MapFrom(r => r.Created))
                .ForMember(d => d.Name, o => o.MapFrom(r => r.Name))
                .ForMember(d => d.Type, o => o.MapFrom(r => "component"));
        }
    }
}
