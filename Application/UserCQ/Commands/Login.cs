using Application.Exceptions;
using Application.Interfaces;
using Application.DTO;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Persistence;
using System.Linq;

namespace Application.UserCQ.Commands
{
    public class Login
    {
        public class Command : IRequest<CurrentUserDTO>
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Command>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command, CurrentUserDTO>
        {
            private readonly UserManager<User> _userManager;
            private readonly SignInManager<User> _signInManager;
            private readonly IMapper mapper;
            iJWTGenerator jWTGenerator;
            DataContext db;
            public Handler(DataContext db, UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, iJWTGenerator _iJWTGenerator)
            {
                this.mapper = mapper;
                this._signInManager = signInManager;
                this._userManager = userManager;
                this.jWTGenerator = _iJWTGenerator;
                this.db = db;
            }

            public async Task<CurrentUserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { User = "Unauthorized" });

                var result = await _signInManager
                    .CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    user.RefreshToken = jWTGenerator.GenerateRefreshToken();
                    user.RefreshTokenExpiryTime = DateTime.Now.AddDays(5);
                    await db.SaveChangesAsync();
                    var res = mapper.Map<User, CurrentUserDTO>(user);
                    res.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault() ?? "";
                    return res;
                }

                throw new RestException(HttpStatusCode.Unauthorized, new { User = "Unauthorized" });
            }
        }
    }
}
