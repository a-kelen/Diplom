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

namespace Application.UserCQ.Queries
{
    public class TopList
    {
        public class Query : IRequest<List<UserDTO>>
        {

        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
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
                var res = await db.Users
                    .Include(x => x.Followers)
                    .Include(x => x.Libraries)
                    .Include(x => x.Components)
                    .OrderByDescending(x => x.Followers.Count * (x.Components.Count + x.Libraries.Count))
                    .Take(50).ToListAsync();

                return mapper.Map <List<User>, List<UserDTO>>(res);
            }
        }
    }
}
