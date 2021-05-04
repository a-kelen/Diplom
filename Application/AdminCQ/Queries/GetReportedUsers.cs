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
    public class GetReportedUsers
    {
        public class Query : IRequest<UserReportsPageDTO>
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
        public class Handler : IRequestHandler<Query, UserReportsPageDTO>
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

            public async Task<UserReportsPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {

                UserReportsPageDTO res = new UserReportsPageDTO();
                res.PageSize = request.PageSize;
                res.CurrentPage = request.NumberPage;
                var reports = await db.UserReports
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Reports = mapper.Map<List<UserReport>, List<UserReportDTO>>(reports);
                return res;
            }
        }
    }
}
