using Application.Exceptions;
using Application.Interfaces;
using Application.DTO;
using Application.Util;
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
using AutoMapper;

namespace Application.UserCQ.Commands
{
    public class Register
    {
        public class Command : IRequest<CurrentUserDTO>
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.Firstname).NotEmpty();
                RuleFor(x => x.Lastname).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, CurrentUserDTO>
        {
            private readonly DataContext _context;
            private readonly UserManager<User> _userManager;
            private readonly IMapper mapper;
            iJWTGenerator jWTGenerator;
            public Handler(DataContext context, UserManager<User> userManager, IMapper mapper, iJWTGenerator _iJWTGenerator)
            {
                this.mapper = mapper;
                this._userManager = userManager;
                this._context = context;
                this.jWTGenerator = _iJWTGenerator;
            }

            public async Task<CurrentUserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });

                if (await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });

                var user = new User
                {
                    Email = request.Email,
                    UserName = request.Username,
                    Firstname = request.Firstname,
                    Lastname = request.Lastname
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    user.RefreshToken = jWTGenerator.GenerateRefreshToken();
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
                    await _context.SaveChangesAsync();
                    return mapper.Map<User, CurrentUserDTO>(user);
                }

                throw new Exception("Problem creating user");
            }
        }
    }
}
