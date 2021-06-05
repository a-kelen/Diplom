using Application.Exceptions;
using Application.Interfaces;
using Application.DTO;
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
using Microsoft.AspNetCore.Identity;
using System.Linq;

namespace Application.UserCQ.Queries
{
    public class Current
    {
        public class Query : IRequest<CurrentUserDTO>
        {

        }
        public class Handler : IRequestHandler<Query, CurrentUserDTO>
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

            public async Task<CurrentUserDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = userAccesor.GetUser();
                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

                var res = mapper.Map<User, CurrentUserDTO>(user);
                res.Role = (await userManager.GetRolesAsync(user)).FirstOrDefault() ?? "";
                return res;
            }
        }
    }
}
