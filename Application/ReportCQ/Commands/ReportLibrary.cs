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
    public class ReportToLibrary
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
                var reportedLibary = await db.Libraries.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (reportedLibary == null)
                    throw new RestException(HttpStatusCode.NotFound, new { UserReport = "NotFound" });
                
                var lr = new LibraryReport { UserId = userId, LibraryId = reportedLibary.Id , Content = request.Content };
                await db.LibraryReports.AddAsync(lr);
                await db.SaveChangesAsync();

                return true;
            }
        }
    }
}
