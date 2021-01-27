using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Application.Exceptions;
using System.Net;
using Microsoft.EntityFrameworkCore;

namespace Application.LibraryCQ.Commands
{
    public class SoftDelete
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
            public class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
            public class Handler : IRequestHandler<Command, bool>
            {
                DataContext db;
                iUserAccessor userAccessor;
                public Handler(DataContext dataContext,
                               iUserAccessor userAccessor)
                {
                    this.db = dataContext;
                    this.userAccessor = userAccessor;
                }

                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    var userId = userAccessor.GetId();
                    Library library = db.Libraries.Include(x => x.Components).FirstOrDefault(x => x.Id == request.Id);
                    
                    if (library == null)
                        throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                    if (userId != library.UserId)
                        throw new RestException(HttpStatusCode.NotFound, new { Library = "Denied" });

                    library.Deleted = !library.Deleted;
                    foreach(var com in library.Components)
                    {
                        com.Deleted = library.Deleted;
                    }
                    await db.SaveChangesAsync();
                    return library.Deleted;
                }
            }
        }
    }
}
