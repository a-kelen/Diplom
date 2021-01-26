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

namespace Application
{
    public class QE
    {
        public class Query : IRequest<ComponentDTO>
        {

        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
        }
        public class Handler : IRequestHandler<Query, ComponentDTO>
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

            public async Task<ComponentDTO> Handle(Query request, CancellationToken cancellationToken)
            {

                var res = new Component();
                return mapper.Map<Component, ComponentDTO>(res);
            }
        }
    }
}
