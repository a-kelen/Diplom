using Application.DTO;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.AdminCQ.Data
{
    public class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<UserReport, UserReportDTO>();
            CreateMap<LibraryReport, LibraryReportDTO>();
            CreateMap<ComponentReport, ComponentReportDTO>();
        }
    }
}
