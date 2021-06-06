using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Application.Notifications;
using Application.ViewModel;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LibraryCQ.Commands
{
    public class Create
    {
        public class Command : IRequest<LibraryDTO>
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string Type { get; set; }
            public bool Status { get; set; }
            public List<ComponentVM> Components { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Name).NotEmpty();
                
            }
        }
        public class Handler : IRequestHandler<Command, LibraryDTO>
        {
            DataContext db;
            iUserAccessor userAccessor;
            IMapper mapper;
            IMediator Mediator;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccessor
                           , IMapper mapper
                           , IMediator mediator)
            {
                this.db = dataContext;
                this.userAccessor = userAccessor;
                this.mapper = mapper;
                this.Mediator = mediator;
            }

            public async Task<LibraryDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetId();
                if (db.UserBlocks.Where(x => x.PersonId == userId).Count() > 0)
                    throw new RestException(HttpStatusCode.BadRequest, new { Component = "Denied" });
                Library library = mapper.Map<Command, Library>(request);

                if(await db.Libraries.CountAsync(x => x.Name == library.Name && x.UserId == userId ) > 0)
                    throw new RestException(HttpStatusCode.BadRequest, new { Library = "Exists" });

                library.Type = Enum.Parse<ElementType>(request.Type);
                library.UserId = userId;
                var res = await db.Libraries.AddAsync(library);
                await db.SaveChangesAsync();
                await Mediator.Publish(new HistoryNotification { ElementId = res.Entity.Id, Type = HistoryType.Library, Action = HistoryAction.Created });
                return mapper.Map<Library, LibraryDTO>(res.Entity);
            }
        }
    }
}
