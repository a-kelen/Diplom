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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.AdminCQ.Commands
{
    public class BlockElement
    {
        public class Command : IRequest<bool>
        {
            public Guid ReportId { get; set; }
            public string ReportType { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.ReportType).NotEmpty();
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
                var moderatorId = userAccessor.GetId();
                if (request.ReportType == "library")
                {
                    if (await db.Libraries.FindAsync(request.ReportId) == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Library = "Not Found" });
                    db.LibraryBlocks.Add(new LibraryBlock { LibraryId = request.ReportId, UserId = moderatorId });
                }
                else if (request.ReportType == "component")
                {
                    if (await db.Components.FindAsync(request.ReportId) == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Component = "Not Found" });
                    db.ComponentBlocks.Add(new ComponentBlock { ComponentId = request.ReportId, UserId = moderatorId });
                }
                else
                    throw new RestException(HttpStatusCode.BadRequest, new { Type = "Not identified" });

                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}
