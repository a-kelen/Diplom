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

namespace Application.LibraryCQ.Queries
{
    public class GetByAuthorAndName
    {
        public class Query : IRequest<DetailedLibraryDTO>
        {
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
        public class Handler : IRequestHandler<Query, DetailedLibraryDTO>
        {
            DataContext db;
            IMapper mapper;
            iUserAccessor userAccessor;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.mapper = mapper;
                this.userAccessor = userAccessor;
            }

            public async Task<DetailedLibraryDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetId();
                var library = db.Libraries
                    .Include(x => x.Components)
                    .Include(x => x.Owner)
                    .Include(x => x.Labels)
                    .FirstOrDefault(x => x.Name == request.Name && x.Owner.UserName == request.Author);

                if (library == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                if ( (library.Deleted || library.Status == false) && library.UserId != userId)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Denied" });

                return mapper.Map<Library, DetailedLibraryDTO>(library);
            }
        }
    }
}
