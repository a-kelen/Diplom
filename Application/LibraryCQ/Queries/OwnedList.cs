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

namespace Application.LibraryCQ.Queries
{
    public class OwnedList
    {
        public class Query : IRequest<List<LibraryDTO>>
        {

        }
        
        public class Handler : IRequestHandler<Query, List<LibraryDTO>>
        {
            DataContext db;
            iUserAccessor userAccesor;
            IMapper mapper;
            public Handler(DataContext dataContext
                           , iUserAccessor userAccesor
                           , IMapper mapper)
            {
                this.db = dataContext;
                this.userAccesor = userAccesor;
                this.mapper = mapper;
            }

            public async Task<List<LibraryDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var id = userAccesor.GetId();
                var res = db.OwnedLibraries
                    .Where(x => x.UserId == id)
                    .Include(x => x.Library).ThenInclude(x => x.Components)
                    .Include(x => x.Library).ThenInclude(x => x.Owner)
                    .Select(x => x.Library).ToList();

                return mapper.Map<List<Library>, List<LibraryDTO>>(res);
            }
        }
    }
}
