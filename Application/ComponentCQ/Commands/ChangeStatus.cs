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

namespace Application.ComponentCQ.Commands
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
                Component component = await db.Components.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (component == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Not found" });
                if (userId != component.UserId || component.LibraryId != null)
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Denied" });
                
                component.Status = !component.Status;
                await db.SaveChangesAsync();
                return component.Status;
            }
        }
    }
}
