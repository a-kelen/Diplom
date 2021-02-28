using Application.DTO;
using Application.Interfaces;
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
                Library library = mapper.Map<Command, Library>(request);
                library.UserId = userAccessor.GetId();
                var res = await db.Libraries.AddAsync(library);
                await db.SaveChangesAsync();

                return mapper.Map<Library, LibraryDTO>(res.Entity);
            }
        }
    }
}
