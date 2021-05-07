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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.UserCQ.Commands
{
    public class EditProfile
    {
        public class Command : IRequest<UserDTO>
        {
            public string Firstname { get; set; }
            public string Lastname { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Firstname).NotEmpty();
                RuleFor(x => x.Lastname).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, UserDTO>
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

            public async Task<UserDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = userAccessor.GetUser();
                db.Entry(user).CurrentValues.SetValues(request);
                await db.SaveChangesAsync();
                return mapper.Map <User, UserDTO> (user);
            }
        }
    }
}
