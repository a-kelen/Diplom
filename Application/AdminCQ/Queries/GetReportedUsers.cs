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
        public class Query : IRequest<ReportedUsersPageDTO>
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
        public class Handler : IRequestHandler<Query, ReportedUsersPageDTO>
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

            public async Task<ReportedUsersPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {

                ReportedUsersPageDTO res = new ReportedUsersPageDTO();
                res.PageSize = request.PageSize;
                res.CurrentPage = request.NumberPage;
                res.TotalReports = await db.Users
                    .Include(x => x.UserReports)
                    .CountAsync(x => x.UserReports.Count > 0);
                var users = await db.Users
                    .Include(x => x.UserReports)
                    .Include(x => x.Block)
                    .Where(x => x.UserReports.Count > 0)
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Users = mapper.Map<List<User>, List<ReportedUserDTO>>(users);
                return res;
            }
        }
    }
}
