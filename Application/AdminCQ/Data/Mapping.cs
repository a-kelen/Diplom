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

            CreateMap<UserReport, UserReportDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status.ToString()))
                .ForMember(d => d.Username, o => o.MapFrom(r => r.User.UserName));
            CreateMap<LibraryReport, LibraryReportDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status.ToString()))
                .ForMember(d => d.Username, o => o.MapFrom(r => r.User.UserName));
            CreateMap<ComponentReport, ComponentReportDTO>()
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Status.ToString()))
                .ForMember(d => d.Username, o => o.MapFrom(r => r.User.UserName));

            CreateMap<User, TableUserDTO>()
                .ForMember(d => d.Name, o => o.MapFrom(r => r.Firstname + " " + r.Lastname))
                .ForMember(d => d.Username, o => o.MapFrom(r => r.UserName))
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Block == null ? "Active" : "Blocked"))
                .AfterMap<TableUserAction>();

            CreateMap<User, ReportedUserDTO>()
                .ForMember(d => d.Name, o => o.MapFrom(r => r.Firstname + " " + r.Lastname))
                .ForMember(d => d.Username, o => o.MapFrom(r => r.UserName))
                .ForMember(d => d.ReportsCount, o => o.MapFrom(r => r.UserReports.Count))
            .ForMember(d => d.Status, o => o.MapFrom(r => r.Block == null ? "Active" : "Blocked"));

            CreateMap<Component, ReportedComponentDTO>()
                .ForMember(d => d.ReportsCount, o => o.MapFrom(r => r.Reports.Count))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName))
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Block == null ? "Active" : "Blocked"));

            CreateMap<Library, ReportedLibraryDTO>()
                .ForMember(d => d.ReportsCount, o => o.MapFrom(r => r.Reports.Count))
                .ForMember(d => d.Status, o => o.MapFrom(r => r.Block == null ? "Active" : "Blocked"))
                .ForMember(d => d.Author, o => o.MapFrom(r => r.Owner.UserName));
        }
    }
}
