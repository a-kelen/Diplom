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
    public class GetReportedComponents
    {
        public class Query : IRequest<ReportedComponentsPageDTO>
        {
            public Guid ComponentId { get; set; }
            public int NumberPage { get; set; } = 0;
            public int PageSize { get; set; } = 10;
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, ReportedComponentsPageDTO>
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

            public async Task<ReportedComponentsPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                ReportedComponentsPageDTO res = new ReportedComponentsPageDTO();
                res.PageSize = request.PageSize;
                res.CurrentPage = request.NumberPage;
                var components = await db.Components
                    .Include(x => x.Reports)
                    .Include(x => x.Owner)
                    .Where(x => x.Reports.Count > 0)
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Components = mapper.Map<List<Component>, List<ReportedComponentDTO>>(components);
                return res;
            }
        }
    }
}
