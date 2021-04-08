using Application.Exceptions;
using Application.Interfaces;
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

namespace Application.UserCQ.Commands
{
    public class ReportToUser
    {
        public class Command : IRequest<bool>
        {
            public string Username { get; set; }
            public string Content { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Content).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, bool>
        {
            DataContext db;
            iUserAccessor userAccessor;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                            )
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
            }

            public async Task<bool> Handle(Command request, CancellationToken cancellationToken)
            {
                var currentUserId = userAccessor.GetId();
                User user = await db.Users.FirstOrDefaultAsync(x => x.UserName == request.Username);

                if (user == null)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Not found" });

                if (user.Id == currentUserId)
                    throw new RestException(HttpStatusCode.NotFound, new { User = "Denied" });

                UserReport report = new UserReport
                {
                    UserId = currentUserId,
                    PersonId = user.Id,
                    Content = request.Content
                };
                await db.UserReports.AddAsync(report);
                return await db.SaveChangesAsync() > 0;
            }
        }
    }
}
