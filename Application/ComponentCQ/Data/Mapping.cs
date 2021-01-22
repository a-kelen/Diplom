using Application.DTO;
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
            CreateMap<Component, ComponentDTO>();
        }
    }
}
