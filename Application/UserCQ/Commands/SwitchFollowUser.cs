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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Commands
{
    public class SwitchFollowUser
    {
        public class Command : IRequest<bool>
        {
            public string Username { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetId();
                var person = await db.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

                if (person == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });
                if(person.Id == userId)
                    throw new RestException(HttpStatusCode.NotFound, new { Operation = "Denied" });

                var follower = await db.Followers.FirstOrDefaultAsync(x => x.UserId == userId && x.PersonId == person.Id);
                bool result = false;
                if (follower == null)
                {
                    await db.Followers.AddAsync(new Follower { UserId = userId, PersonId = person.Id });
                    result = true;
                }
                else
                {
                    db.Followers.Remove(follower);
                }
                await db.SaveChangesAsync();
                return result;
            }
        }
    }
}
