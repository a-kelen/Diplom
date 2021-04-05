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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LibraryCQ.Queries
{
    public class GetLibraryAvatar
    {
        public class Query : IRequest<byte[]>
        {
            public Guid LibraryId { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

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
                var library = await db.Libraries.FirstOrDefaultAsync(x => x.Id == request.LibraryId);

                if (library == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });

                if (library.Avatar == "")
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Don`t have avatar" });

                string path = Path.Combine(environment.WebRootPath, "Avatars", library.Avatar);
                var image = await System.IO.File.ReadAllBytesAsync(path);
                return image;
            }
        }
    }
}
