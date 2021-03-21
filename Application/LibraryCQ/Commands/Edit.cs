using Application.DTO;
using Application.Exceptions;
using Application.Interfaces;
using Application.ViewModel;
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

namespace Application.LibraryCQ.Commands
{
    public class Edit
    {
        public class Command : IRequest<LibraryDTO>
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                //RuleFor(x => x.Name).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, LibraryDTO>
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

            public async Task<LibraryDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = userAccessor.GetId();
                Library library = await db.Libraries.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (library == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                if (userId != library.UserId)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Denied" });

                db.Entry(library).CurrentValues.SetValues(request);
                await db.SaveChangesAsync();
                return mapper.Map<Library, LibraryDTO>(library);
            }
        }
    }
}
