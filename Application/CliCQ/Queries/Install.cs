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
    public class Install
    {
        public class Query : IRequest<FolderDTO>
        {
            public string Type { get; set; }
            public string Author { get; set; }
            public string Name { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Author).NotEmpty();
                RuleFor(x => x.Name).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Query, FolderDTO>
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

            public async Task<FolderDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = new FolderDTO();
                if(request.Type == "component")
                {
                    Component component = await db.Components
                        .Include(x => x.Owner)
                        .FirstOrDefaultAsync(x => x.Owner.UserName == request.Author && x.Name == request.Name);
                    res.Name = component.Name;
                } 
                else if(request.Type == "library")
                {
                    Library library = await db.Libraries
                        .Include(x => x.Owner)
                        .FirstOrDefaultAsync(x => x.Owner.UserName == request.Author && x.Name == request.Name);
                    res.Name = library.Name;
                    res.Components = library.Components.Select(x => x.Name).ToList();
                }
                return res;
            }
        }
    }
}
