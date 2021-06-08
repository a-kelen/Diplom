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

namespace Application.LabelCQ
{
    public class GetLabels
    {
        public class Query : IRequest<List<string>>
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

                return await db.Labels
                    .Where(x => x.Name.Contains(request.SearchQuery))
                    .Select(x => x.Name)
                    .ToListAsync();
            }
        }
    }
}
