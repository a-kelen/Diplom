using Application.DTO;
using Application.Interfaces;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
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
            public string Description { get; set;}
            public List<EventVM> Events { get; set; }
            public List<PropVM> Props { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Description).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, ComponentDTO>
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

            public async Task<ComponentDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                Component component = mapper.Map<Command, Component>(request);
                component.UserId = userAccessor.GetId();
                var res = await db.Components.AddAsync(component);
                await db.SaveChangesAsync();
                return mapper.Map<Component, ComponentDTO>(res.Entity);
            }
        }
    }
}
