﻿using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.FileCQ.Commands
{
    public class UploadFile
    {
        public class Command : IRequest<bool>
        {
            public Guid ElementId { get; set; }
            public string Descriminator { get; set; }
            public IList<IFormFile> Files { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Descriminator).NotEmpty();
                RuleFor(x => x.ElementId).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                List<File> Files = new List<File>();
                foreach (var f in request.Files)
                {
                    Files.Add(new File { Path = f.FileName });
                }

                if (request.Descriminator == "component")
                {
                    var component = await db.Components.FirstOrDefaultAsync(x => x.Id == request.ElementId);
                    if (component == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Component = "Not found" });
                    component.File = Files[0].Path;
                } else if(request.Descriminator == "library")
                {
                    var library = await db.Libraries.FirstOrDefaultAsync(x => x.Id == request.ElementId);
                    if (library == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                    library.File = Files[0].Path;
                } else
                throw new RestException(HttpStatusCode.NotFound, new { Descriminator = "Error" });
                
                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}