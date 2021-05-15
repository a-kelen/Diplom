using Application.DTO;
using Application.Interfaces;
using Application.Notifications;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComponentCQ.Commands
{
    public class Create
    {
        public class Command : IRequest<ComponentDTO>
        {
            public bool Status { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set;}
            public string Dependencies { get; set; }
            public List<PropVM> Props { get; set; }
            public List<SlotVM> Slots { get; set; }
            public List<EventVM> Events { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                //RuleFor(x => x.Description).NotEmpty();
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
                Component component = mapper.Map<Command, Component>(request);
                component.Type = Enum.Parse<ElementType>(request.Type);
                component.UserId = userAccessor.GetId();
                var res = await db.Components.AddAsync(component);
                await db.SaveChangesAsync();
                await Mediator.Publish(new HistoryNotification { ElementId = res.Entity.Id, Type = HistoryType.Component, Action = HistoryAction.Created });
                return mapper.Map<Component, ComponentDTO>(res.Entity);
            }
        }
    }
}
