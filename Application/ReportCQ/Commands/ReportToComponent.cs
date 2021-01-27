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

namespace Application.ReportCQ.Commands
{
    public class ReportToComponent
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string Content { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotNull();
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
                var userId = userAccessor.GetId();
                var reportedComponent = await db.Components.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (reportedComponent == null || reportedComponent.LibraryId != null)
                    throw new RestException(HttpStatusCode.NotFound, new { UserReport = "NotFound" });

                var cr = new ComponentReport { UserId = userId, ComponentId = reportedComponent.Id, Content = request.Content };
                await db.ComponentReports.AddAsync(cr);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
