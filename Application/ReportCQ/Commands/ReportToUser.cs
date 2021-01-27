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
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.ReportCQ.Commands
{
    public class ReportToUser
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
            public string Content { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Id).NotNull();
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
                var userId = userAccessor.GetId();
                var reportedUser = await db.Users.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (request.Id == userId || reportedUser != null)
                    throw new RestException(HttpStatusCode.Unauthorized, new { UserReport = "Denied" });
                
                var ur = new UserReport { UserId = userId, PersonId = reportedUser.Id, Content = request.Content };
                await db.UserReports.AddAsync(ur);
                await db.SaveChangesAsync();
                return true;
            }
        }
    }
}
