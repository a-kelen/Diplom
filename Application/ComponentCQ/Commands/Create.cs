using Application.DTO;
using Application.Interfaces;
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

            public List<EventDTO> Events { get; set; }
            public List<PropDTO> Props { get; set; }

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

                //db.Components.Add(request.);

                return mapper.Map<Component, ComponentDTO>(new Component());
            }
        }
    }
}
