using Application.Interfaces;
using AutoMapper;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ComponentCQ.Commands
{
    public class SoftDelete
    {
        public class Command : IRequest<bool>
        {
            public Guid Id
            {
                get; set;

            }
            public class Validator : AbstractValidator<Command>
            {
                public Validator()
                {
                    RuleFor(x => x.Id).NotEmpty();
                }
            }
            public class Handler : IRequestHandler<Command, bool>
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

                public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
                {
                    return true;
                }
            }
        }
    }
}
