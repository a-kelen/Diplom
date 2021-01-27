using Application.Exceptions;
using Application.Interfaces;
using Application.DTO;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace Application.UserCQ.Queries
{
    public class OwnList
    {
        public class Query : IRequest<List<ComponentDTO>>
        {

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
                var userId = userAccesor.GetId();

                var res = db.Components.Where(x => x.UserId == userId).ToList();
                return  mapper.Map<List<Component>, List<ComponentDTO>>(res);
            }
        }
    }
}
