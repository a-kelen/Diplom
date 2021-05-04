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
        public class Query : IRequest<ComponentReportsPageDTO>
        {
            public int NumberPage { get; set; } = 1;
            public int PageSize { get; set; } = 10;
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, ComponentReportsPageDTO>
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

            public async Task<ComponentReportsPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                ComponentReportsPageDTO res = new ComponentReportsPageDTO();
                res.PageSize = request.PageSize;
                res.CurrentPage = request.NumberPage;
                var reports = await db.ComponentReports
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Reports = mapper.Map<List<ComponentReport>, List<ComponentReportDTO>>(reports);
                return res;
            }
        }
    }
}
