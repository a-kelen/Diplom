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
    public class SearchComponents
    {
        public class Query : IRequest<List<ComponentDTO>>
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

                var q = db.Components
                    .Include(x => x.Owner)
                    .Include(x => x.Labels)
                    .Where(x => 
                        x.Status == true && 
                        x.UserId != null
                    );
                if(request.SearchQuery != null) {
                    q = q.Where(x => x.Name.Contains(request.SearchQuery));
                }

                List<Component> res = new List<Component>();
                if (request.Labels != null)
                {
                    q = q.Where(x => x.Labels.Any(t => request.Labels.Contains(t.Name)));
                    res = await q.ToListAsync();
                }
                else if (request.SearchQuery == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Search = "Invalid" });


                return mapper.Map <List<Component>, List<ComponentDTO>> (res);
            }
        }
    }
}
