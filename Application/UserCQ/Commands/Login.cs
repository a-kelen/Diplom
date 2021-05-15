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
            public Handler(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper)
            {
                this.mapper = mapper;
                this._signInManager = signInManager;
                this._userManager = userManager;
            }

            public async Task<CurrentUserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new RestException(HttpStatusCode.Unauthorized, null);

                var result = await _signInManager
                    .CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    return mapper.Map<User, CurrentUserDTO>(user);
                }

                throw new RestException(HttpStatusCode.Unauthorized, null);
            }
        }
    }
}
