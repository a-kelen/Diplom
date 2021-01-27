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

namespace Application.LibraryCQ.Commands
{
    public class ChangeStatus
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
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
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor)
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
                if (userId != library.UserId)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Denied" });

                library.Status = !library.Status;
                await db.SaveChangesAsync();
                return library.Status;
            }
        }
    }
}
