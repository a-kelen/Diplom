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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Commands
{
    public class ChangePassword
    {
        public class Command : IRequest<bool>
        {
            public string OldPassword { get; set; }
            public string NewPassword { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.OldPassword).NotEmpty();
                RuleFor(x => x.NewPassword).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            DataContext db;
            iUserAccessor userAccessor;
            UserManager<User> manager;
            IPasswordHasher<User> hasher;
            IPasswordValidator<User> validator;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper
                           , UserManager<User> manager
                           , IPasswordHasher<User> hasher
                           , IPasswordValidator<User> validator)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
                this.manager = manager;
                this.hasher = hasher;
                this.validator = validator;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = userAccessor.GetUser();
                
                var pass1 = await manager.CheckPasswordAsync(user, request.OldPassword);
                if (pass1)
                {
                    IdentityResult result = await validator.ValidateAsync(manager, user, request.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = hasher.HashPassword(user, request.NewPassword);
                        await manager.UpdateAsync(user);
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
