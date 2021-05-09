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
    public class GetDependencies
    {
        public class Query : IRequest<List<string>>
        {
            public string Type { get; set; }
            public string Author { get; set; }
            public string Name { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Type).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Query, List<string>>
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

            public async Task<List<string>> Handle(Query request, CancellationToken cancellationToken)
            {

                List<string> res = null;
                if(request.Type == "component") {
                   Component component = await db.Components
                        .Include(x => x.Owner)
                        .FirstOrDefaultAsync(x => x.Owner.UserName == request.Author && x.Name == request.Name);
                    res = new List<string>(component.Dependencies.Split("\n"));
                } 
                else if(request.Type == "library") {
                    HashSet<string> deps = new HashSet<string>();
                    Library library = await db.Libraries
                        .Include(x => x.Owner)
                        .Include(x => x.Components)
                        .FirstOrDefaultAsync(x => x.Owner.UserName == request.Author && x.Name == request.Name);

                    foreach (Component c in library.Components)
                        if(c.Dependencies != null)
                            deps.UnionWith(c.Dependencies.Split("\n"));

                    res = new List<string>(deps);
                }

                return res;
            }
        }
    }
}
