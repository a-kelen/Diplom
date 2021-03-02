using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.LikeCQ.Data
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Like, LikeDTO>()
                //.ForMember(d => d.DateTime, o => o.MapFrom(s => s.cre))
                .AfterMap<ElementAction>();
        }
    }
}
