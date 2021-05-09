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
    public class GetFollowedUsers
    {
        public class Query : IRequest<List<FollowDTO>>
        {

        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, List<FollowDTO>>
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

            public async Task<List<FollowDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccesor.GetId();
                var ids = db.Followers.Where(x => x.UserId == userId).Select(x => x.PersonId);

                var res = await db.Users.Where(x => ids.Contains(x.Id)).ToListAsync();
                return mapper.Map <List<User>, List<FollowDTO>> (res);
            }
        }
    }
}
