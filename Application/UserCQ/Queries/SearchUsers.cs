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

namespace Application.UserCQ.Queries
{
    public class SearchUsers
    {
        public class Query : IRequest<List<UserDTO>>
        {
            public string SearchQuery { get; set; }
        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, List<UserDTO>>
        {
            DataContext db;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.mapper = mapper;
            }

            public async Task<List<UserDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var res = await db.Users.Where(x => x.UserName.Contains(request.SearchQuery)).ToListAsync();
                return mapper.Map<List<User>, List<UserDTO>>(res);
            }
        }
    }
}
