using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Application.Notifications;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComponentCQ.Commands
{
    public class ToOwnComponent
    {
        public class Command : IRequest<ComponentDTO>
        {
            public Guid Id { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                
            }
        }
        public class Handler : IRequestHandler<Command, ComponentDTO>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            IMediator Mediator;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper
                           , IMediator mediator)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
                this.Mediator = mediator;
            }

            public async Task<ComponentDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = userAccessor.GetUser();
                var component = await db.Components.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (component == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Not found" });
                if (user.Id == component.UserId)
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Denied" });
                if (db.OwnedComponents.Any(x => x.ComponentId == component.Id && x.UserId == user.Id))
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Exist" });

                db.OwnedComponents.Add(new OwnedComponent { ComponentId = component.Id, UserId = user.Id });
                db.SaveChanges();
                await Mediator.Publish(new HistoryNotification { ElementId = component.Id, Type = HistoryType.Component, Action = HistoryAction.Owned });
                return mapper.Map<Component, ComponentDTO>(component);
            }
        }
    }
}
