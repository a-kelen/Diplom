using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AdminCQ.Queries
{
    public class GetReportedLibraries
    {
        public class Query : IRequest<LibraryReportsPageDTO>
        {
            public int NumberPage { get; set; }
            public int PageSize { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, LibraryReportsPageDTO>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
            }

            public async Task<LibraryReportsPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                LibraryReportsPageDTO res = new LibraryReportsPageDTO();
                res.PageSize = request.PageSize;
                res.CurrentPage = request.NumberPage;
                var reports = await db.LibraryReports
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Reports = mapper.Map<List<LibraryReport>, List<LibraryReportDTO>>(reports);
                return res;
            }
        }
    }
}
