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

namespace Application.AdminCQ.Commands
{
    public class SwitchBlockUser
    {
        public class Command : IRequest<TableUserDTO>
        {
            public string Email { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Email).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, TableUserDTO>
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

            public async Task<TableUserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var moderatorId = userAccessor.GetId();
                var user = await db.Users.Include(x => x.Block).FirstOrDefaultAsync(x => x.Email == request.Email);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

                if(user.Block == null)
                {
                    db.UserBlocks.Add(new UserBlock { PersonId = user.Id, UserId = moderatorId });
                } else
                {
                    db.UserBlocks.Remove(user.Block);
                }

                await db.SaveChangesAsync();
                return mapper.Map<User, TableUserDTO>(user);
            }
        }
    }
}
