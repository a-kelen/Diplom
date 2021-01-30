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

namespace Application.ComponentCQ.Commands
{
    public class ComponentLike
    {
        public class Command : IRequest<bool>
        {
            public Guid Id { get; set; }
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
                var component = db.Components.FirstOrDefaultAsync(x => x.Id == request.Id);
                var like = await db.Likes.FirstOrDefaultAsync(x => x.UserId == userId && x.ElementId == request.Id);

                if (component == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });

                if (like == null)
                {
                    var l = new Like { UserId = userId, ElementId = like.Id, Descriminator = LikeDescriminator.Component };
                    await db.Likes.AddAsync(l);
                }
                else
                {
                    db.Likes.Remove(like);
                }
                await db.SaveChangesAsync();
                return like == null;
            }
        }
    }
}
