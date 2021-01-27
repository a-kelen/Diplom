﻿using Application.DTO;
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
            iUserAccessor userAccessor;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.mapper = mapper;
                this.userAccessor = userAccessor;
            }

            public async Task<LibraryDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetId();
                var res = db.Libraries.FirstOrDefault(x => x.Id == request.Id);

                if (res == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                if(res.Deleted && res.UserId != userId)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Denied" });

                return mapper.Map<Library, LibraryDTO>(res);
            }
        }
    }
}
