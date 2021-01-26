using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
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
    public class GetById
    {
        public class Query : IRequest<LibraryDTO>
        {
            public Guid Id { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotNull();
            }
        }
        public class Handler : IRequestHandler<Query, LibraryDTO>
        {
            DataContext db;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.mapper = mapper;
            }

            public async Task<LibraryDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = db.Libraries.FirstOrDefault(x => x.Id == request.Id);

                return mapper.Map<Library, LibraryDTO>(res);
            }
        }
    }
}
