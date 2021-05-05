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
    public class Get
    {
        public class Query : IRequest<DetailedUserDTO>
        {
            public string Username { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Username).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Query, DetailedUserDTO>
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

            public async Task<DetailedUserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = db.Users
                    .Include(x => x.Components.Where(t => t.Status == true))
                    .Include(x => x.Libraries.Where(t => t.Status == true))
                    .Include(x => x.Followers)
                    .Include(x => x.Follows)
                    .FirstOrDefault(x => x.UserName == request.Username);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

                return mapper.Map <User, DetailedUserDTO> (user);
            }
        }
    }
}
