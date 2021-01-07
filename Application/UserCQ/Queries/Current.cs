using Application.Exceptions;
using Application.Interfaces;
using Application.UserCQ.Data;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Queries
{
    public class Current
    {
        public class Query : IRequest<UserDTO>
        {

        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, UserDTO>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            private readonly iJWTGenerator _jwtGenerator;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper
                           , iJWTGenerator jwtGenerator)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
                this._jwtGenerator = jwtGenerator;
            }

            public async Task<UserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = userAccesor.GetUser();
                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });
                return new UserDTO
                {
                    DisplayName = user.Email,
                    Username = user.UserName,
                    Token = _jwtGenerator.CreateToken(user),
                    Image = null
                };
            }
        }
    }
}
