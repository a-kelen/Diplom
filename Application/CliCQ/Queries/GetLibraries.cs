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
    public class GetLibraries
    {
        public class Query : IRequest<List<LibraryDTO>>
        {
            //public string Type { get; set; }
            public int Page { get; set; } = 1;
            public bool My { get; set; } = false;
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                //RuleFor(x => x.Type).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Query, List<LibraryDTO>>
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

            public async Task<List<LibraryDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccesor.GetId();
                List<Library> libraries;
                if(request.My) {
                    var ids = db.OwnedLibraries.Where(x => x.UserId == userId).Select(x => x.LibraryId).ToArray();
                    libraries = await db.Libraries
                        .Where(x => x.UserId == userId || ids.Contains(x.Id))
                        .Skip(request.Page * 10)
                        .Take(10)
                        .ToListAsync();
                } else
                {
                    libraries = await db.Libraries
                        .Where(x => x.UserId == userId)
                        .Skip(request.Page * 10)
                        .Take(10)
                        .ToListAsync();
                }
                return mapper.Map<List<Library>, List<LibraryDTO>>(libraries);
            }
        }
    }
}
