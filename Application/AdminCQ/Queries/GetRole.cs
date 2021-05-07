using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AdminCQ.Queries
{
    public class GetRole
    {
        public class Query : IRequest<string>
        {

        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, string>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            UserManager<User> userManager;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper
                           , UserManager<User> userManager)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
                this.userManager = userManager;
            }

            public async Task<string> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = userAccesor.GetUser();

                var roles = await userManager.GetRolesAsync(user);

                return roles[0];
            }
        }
    }
}
