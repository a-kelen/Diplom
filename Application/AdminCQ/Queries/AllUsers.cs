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
        public class Query : IRequest<List<UserDTO>>
        {
            public int NumberPage { get; set; } = 1;
            public int PageSize { get; set; } = 10;
        }

        public class Handler : IRequestHandler<Query, List<UserDTO>>
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

            public async Task<List<UserDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var currentUser = userAccesor.GetUser();
                var res =  await db.Users.
                    Where(x => x.NormalizedEmail != currentUser.NormalizedEmail)
                    .Skip(request.NumberPage * request.PageSize)
                    .Take(request.PageSize)
                    .ToListAsync();
                return mapper.Map <List<User>, List<UserDTO>> (res);    
            }
        }
    }
}
