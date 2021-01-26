using Application.DTO;
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
            CreateMap<Library, LibraryDTO>();

            CreateMap<Commands.Create.Command, Library>();

            CreateMap<ComponentVM, Component>();


        }
    }
}
