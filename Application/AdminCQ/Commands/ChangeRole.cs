using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Identity;
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
    public class ChangeRole
    {
        public class Command : IRequest<bool>
        {
            public string UserEmail { get; set; }
            public string Role { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.UserEmail).NotEmpty();
                RuleFor(x => x.Role).NotEmpty();
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
                User user = await db.Users.FirstOrDefaultAsync(x => x.Email == request.UserEmail);
                Role role = await db.Roles.FirstOrDefaultAsync(x => x.Name == request.Role);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });
                if (role == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Role = "Not found" });

                db.RemoveRange(db.UserRoles.Where(x => x.UserId == user.Id));
                db.UserRoles.Add(new IdentityUserRole<Guid> { UserId = user.Id, RoleId = role.Id });

                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}
