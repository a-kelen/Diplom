using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
            UserManager<User> userManager;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper
                           , UserManager<User> userManager)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
                this.userManager = userManager;
            }

            public async Task<UsersPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUser = userAccesor.GetUser();
                UsersPageDTO res = new UsersPageDTO();
                res.TotalUsers = await db.Users.CountAsync();
                res.BlockedUsers = await db.UserBlocks.CountAsync();
                var users = await db.Users
                    .Include(x => x.UserReports)
                    .Include(x => x.Block)
                    .Where(x => x.NormalizedEmail != currentUser.NormalizedEmail)
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                res.Users = mapper.Map<List<User>, List<TableUserDTO>>(users); 

                for(int i = 0; i < res.Users.Count; i++)
                {
                    res.Users[i].Role = (await userManager.GetRolesAsync(users[i])).FirstOrDefault() ?? "";
                }
                return res; 
            }
        }
    }
}
