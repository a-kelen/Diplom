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
    public class SearchLibraries
    {
        public class Query : IRequest<List<LibraryDTO>>
        {
            public string SearchQuery { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
                RuleFor(x => x.SearchQuery).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Query, List<LibraryDTO>>
        {
            DataContext db;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.mapper = mapper;
            }

            public async Task<List<LibraryDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = await db.Libraries.Where(x => x.Status == true && x.Name.Contains(request.SearchQuery)).ToListAsync();
                return mapper.Map<List<Library>, List<LibraryDTO>>(res);
            }
        }
    }
}
