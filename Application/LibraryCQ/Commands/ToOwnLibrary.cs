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
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LibraryCQ.Commands
{
    public class ToOwnLibrary
    {
        public class Command : IRequest<LibraryDTO>
        {
            public Guid Id { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {

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
                var user = userAccessor.GetUser();
                var library = await db.Libraries.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (library == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Not found" });
                if (user.Id == library.UserId)
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Denied" });
                if(db.OwnedLibraries.Any(x => x.LibraryId == library.Id && x.UserId == user.Id))
                    throw new RestException(HttpStatusCode.NotFound, new { Library = "Exist" });

                user.OwnedLibraries.Add(new OwnedLibrary { LibraryId = library.Id });
                await db.SaveChangesAsync();
                return mapper.Map<Library, LibraryDTO>(library);
            }
        }
    }
}
