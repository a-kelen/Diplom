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

namespace Application.CliCQ.Queries
{
    public class GetComponents
    {
        public class Query : IRequest<List<ComponentDTO>>
        {
            public string Type { get; set; }
            public int Page { get; set; } = 0;
            public bool My { get; set; } = false;
        }
   
        public class Handler : IRequestHandler<Query, List<ComponentDTO>>
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

            public async Task<List<ComponentDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccesor.GetId();
                List<Component> components;
                if (request.My)
                {
                    var ids = db.OwnedComponents.Where(x => x.UserId == userId).Select(x => x.ComponentId).ToArray();
                    components = await db.Components
                        .Where(x => x.UserId == userId || ids.Contains(x.Id))
                        .Skip(request.Page * 10)
                        .Take(10)
                        .ToListAsync();
                } else
                {
                    components = await db.Components
                        .Where(x => x.UserId == userId)
                        .Skip(request.Page * 10)
                        .Take(10)
                        .ToListAsync();
                }
                return mapper.Map<List<Component>, List<ComponentDTO>>(components);
            }
        }
    }
}
