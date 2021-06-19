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
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Commands
{
    public class RefreshToken
    {
        public class Command : IRequest<CurrentUserDTO>
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.AccessToken).NotEmpty();
                RuleFor(x => x.RefreshToken).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, CurrentUserDTO>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            iJWTGenerator jWTGenerator;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper
                           , iJWTGenerator iJWTGenerator)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
                this.jWTGenerator = iJWTGenerator;
            }

            public async Task<CurrentUserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var principal = jWTGenerator.GetPrincipalFromExpiredToken(request.AccessToken);
                var username = principal.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.NameId).Value;
                var user = await db.Users.FirstOrDefaultAsync(u => u.UserName == username);
                if (user == null || user.RefreshToken != request.RefreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    throw new RestException(HttpStatusCode.Unauthorized, new { User = "Unauthorized" });
                }
                var newAccessToken = jWTGenerator.CreateToken(user);
                var newRefreshToken = jWTGenerator.GenerateRefreshToken();
                user.RefreshToken = newRefreshToken;
                await db.SaveChangesAsync();
                return mapper.Map <User, CurrentUserDTO>(user);
            }
        }
    }
}
