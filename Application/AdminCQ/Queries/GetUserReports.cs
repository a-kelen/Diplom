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
    public class GetUserReports
    {
        public class Query : IRequest<UserReportsPageDTO>
        {
            public string Email { get; set; }
            public int NumberPage { get; set; } = 0;
            public int PageSize { get; set; } = 10;
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty();
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
                var user = await db.Users.FirstOrDefaultAsync(x => x.Email == request.Email);

                UserReportsPageDTO res = new UserReportsPageDTO();
                res.PageSize = request.PageSize;
                res.CurrentPage = request.NumberPage;
                res.TotalReports = await db.UserReports.CountAsync(x => x.PersonId == user.Id);
                res.AdmittedReports = await db.UserReports.CountAsync(x => x.PersonId == user.Id && x.Status == ReportStatus.Admitted);

                var reports = await db.UserReports
                    .Include(x => x.User)
                    .Where(x => x.PersonId == user.Id)
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Reports = mapper.Map<List<UserReport>, List<UserReportDTO>>(reports);
                return res;
            }
        }
    }
}
