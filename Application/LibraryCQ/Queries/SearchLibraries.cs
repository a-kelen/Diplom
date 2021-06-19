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
            public string SearchQuery { get; set; } = "";
            public List<string> Labels { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {
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
                var q = db.Libraries
                    .Include(x => x.Owner)
                    .Include(x => x.Components)
                    .Include(x => x.Labels)
                    .Where(x => x.Status == true);

                if (request.SearchQuery != null)
                {
                    q = q.Where(x => x.Name.Contains(request.SearchQuery));
                }

                List<Library> res = new List<Library>();
                if (request.Labels != null)
                {
                    q = q.Where(x => x.Labels.Any(t => request.Labels.Contains(t.Name)));
                    
                }
                else if (request.SearchQuery == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Search = "Invalid" });
                res = await q.ToListAsync();
                return mapper.Map<List<Library>, List<LibraryDTO>>(res);
            }
        }
    }
}
