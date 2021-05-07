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
        public class Query : IRequest<ReportedLibrariesPageDTO>
        {
            public int NumberPage { get; set; } = 0;
            public int PageSize { get; set; } = 10;
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, ReportedLibrariesPageDTO>
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

            public async Task<ReportedLibrariesPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                ReportedLibrariesPageDTO res = new ReportedLibrariesPageDTO();
                res.PageSize = request.PageSize;
                res.CurrentPage = request.NumberPage;
                res.TotalReports = await db.Libraries
                    .Include(x => x.Reports)
                    .CountAsync(x => x.Reports.Count > 0);
                var libraries = await db.Libraries
                    .Include(x => x.Reports)
                    .Include(x => x.Owner)
                    .Include(x => x.Block)
                    .Where(x => x.Reports.Count > 0)
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Libraries = mapper.Map<List<Library>, List<ReportedLibraryDTO>>(libraries);
                return res;
            }
        }
    }
}
