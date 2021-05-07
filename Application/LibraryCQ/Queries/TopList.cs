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
    public class TopList
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
                var ids = db.Likes.Where(x => x.Descriminator == LikeDescriminator.Library)
                    .GroupBy(x => x.ElementId)
                    .OrderByDescending(x => x.Count())
                    .Take(50)
                    .Select(x => x.Key);

                var res = await db.Libraries
                    .Include(x => x.Owner)
                    .Where(x => ids.Contains(x.Id) && x.Status == true)
                    .ToListAsync();

                return mapper.Map<List<Library>, List<LibraryDTO>>(res);
            }
        }
    }
}
