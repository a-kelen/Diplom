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
    public class AllUsers
    {
        public class Query : IRequest<UsersPageDTO>
        {
            public int NumberPage { get; set; } = 0;
            public int PageSize { get; set; } = 10;
        }

        public class Handler : IRequestHandler<Query, UsersPageDTO>
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

            public async Task<UsersPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUser = userAccesor.GetUser();
                UsersPageDTO res = new UsersPageDTO();
                res.TotalUsers = await db.Users.CountAsync();
                var users = await db.Users
                    .Include(x => x.UserReports)
                    .Include(x => x.Block)
                    .Where(x => x.NormalizedEmail != currentUser.NormalizedEmail)
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Users = mapper.Map<List<User>, List<TableUserDTO>>(users); 
                return res; 
            }
        }
    }
}
