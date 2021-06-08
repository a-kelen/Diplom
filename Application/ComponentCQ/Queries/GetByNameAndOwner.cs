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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComponentCQ.Queries
{
    public class GetByNameAndOwner
    {
        public class Query : IRequest<DetailedComponentDTO>
        {
            public bool Dependent { get; set; }
            public string Owner { get; set; }
            public string Name { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Owner).NotEmpty();
                RuleFor(x => x.Dependent).NotNull();
            }
        }
        public class Handler : IRequestHandler<Query, DetailedComponentDTO>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
            }

            public async Task<DetailedComponentDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                Component component;
                if(request.Dependent)
                {
                    component = db.Components
                    .Include(x => x.Owner)
                    .Include(x => x.Library).ThenInclude(x => x.Owner)
                    .Include(x => x.Events)
                    .Include(x => x.Props)
                    .Include(x => x.Slots)
                    .FirstOrDefault(x => x.Library.Name == request.Owner && x.Name == request.Name);
                } else
                {
                    component = db.Components
                    .Include(x => x.Owner)
                    .Include(x => x.Library).ThenInclude(x => x.Owner)
                    .Include(x => x.Events)
                    .Include(x => x.Props)
                    .Include(x => x.Slots)
                    .Include(x => x.Labels)
                    .FirstOrDefault(x => x.Owner.UserName == request.Owner && x.Name == request.Name);
                }


                return mapper.Map <Component, DetailedComponentDTO> (component);
            }
        }
    }
}
