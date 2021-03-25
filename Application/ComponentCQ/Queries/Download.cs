using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

namespace Application.ComponentCQ.Queries
{
    public class Download
    {
        public class Query : IRequest<FileStreamResult>
        {
            public Guid Id { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, FileStreamResult>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            IWebHostEnvironment environment;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper
                           , IWebHostEnvironment appEnvironment)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
                this.environment = appEnvironment;
            }

            public async Task<FileStreamResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var component = await db.Components.FirstOrDefaultAsync(x => x.Id == request.Id);
                string path = Path.Combine(environment.WebRootPath, "Files", request.Id.ToString());
                if (Directory.Exists(path))
                {
                    path = Path.Combine(path, component.File);
                    byte[] mas = System.IO.File.ReadAllBytes(path);
                    var stream = System.IO.File.OpenRead(path);
                    return new FileStreamResult(stream, "application/octet-stream");
                }

                throw new RestException(HttpStatusCode.NotFound, new { File = "Not found" });
            }
        }
    }
}
