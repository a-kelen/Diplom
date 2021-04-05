using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Queries
{
    public class GetAvatar
    {
        public class Query : IRequest<byte[]>
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
        public class Handler : IRequestHandler<Query, byte[]>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            IWebHostEnvironment environment;
            public Handler(DataContext dataContext,
                           iUserAccessor userAccesor,
                           IMapper mapper,
                           IWebHostEnvironment environment)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
                this.environment = environment;
            }

            public async Task<byte[]> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });
                if (user.Avatar == "")
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Don`t have avatar" });

                string path = Path.Combine(environment.WebRootPath, "Avatars", user.Avatar);
                var image = await System.IO.File.ReadAllBytesAsync(path);
                return image;
            }
        }
    }
}
