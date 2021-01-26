using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application
{
    public class CE
    {
        public class Command : IRequest<object>
        {
            

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
            }
        }
        public class Handler : IRequestHandler<Command, object>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
            }

            public async Task<object> Handle(Command request, CancellationToken cancellationToken)
            {
                
                return mapper.Map<Component, ComponentDTO>(new Component());
            }
        }
    }
}
