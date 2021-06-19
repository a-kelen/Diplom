using Application.Exceptions;
using Application.Interfaces;
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

namespace Application.LibraryCQ.Commands
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
                RuleFor(x => x.Content).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            DataContext db;
            iUserAccessor userAccessor;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                            )
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetId();
                Library library = await db.Libraries.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (library == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
               
                LibraryReport report = new LibraryReport
                {
                    UserId = userId,
                    LibraryId = library.Id,
                    Content = request.Content
                };
                await db.LibraryReports.AddAsync(report);
                
                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}
