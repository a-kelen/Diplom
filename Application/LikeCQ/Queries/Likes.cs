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

namespace Application.LikeCQ.Queries
{
    public class Likes
    {
        public class Query : IRequest<List<LikeDTO>>
        {

        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, List<LikeDTO>>
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

            public async Task<List<LikeDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var id = userAccesor.GetId();
                var res = db.Likes.Where(x => x.UserId == id).ToList();
                res.Reverse();
                return mapper.Map <List<Like>, List<LikeDTO>> (res);
            }
        }
    }
}
