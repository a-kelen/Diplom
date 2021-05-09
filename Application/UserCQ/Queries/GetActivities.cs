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

namespace Application.UserCQ.Queries
{
    public class GetActivities
    {
        public class Query : IRequest<ActivitiesPageDTO>
        {
            public int Page { get; set; } = 0;
        }

        public class Handler : IRequestHandler<Query, ActivitiesPageDTO>
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

            public async Task<ActivitiesPageDTO> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = userAccesor.GetUser();
                db.Entry(user).Collection(t => t.Follows).Load();
                ActivitiesPageDTO res = new ActivitiesPageDTO();
                res.Page = request.Page;
                res.Activities = new List<ActivityDTO>();

                var q = db.Components
                    .Select(x => new { x.Id, x.Created })
                    .Union(db.Libraries.Select(x => new { x.Id, x.Created }))
                    .OrderByDescending(x => x.Created)
                    .Skip(request.Page)
                    .Take(30);

                var componentActivities = await db.Components
                    .Include(x => x.Owner)
                    .Where(x => q.AsQueryable()
                        .Select(x => x.Id)
                        .Contains(x.Id) &&
                        x.LibraryId == null)
                    .ToListAsync();

                var libraryActivities = await db.Libraries
                    .Include(x => x.Owner)
                    .Where(x => q.AsQueryable()
                    .Select(x => x.Id)
                    .Contains(x.Id))
                    .ToListAsync();

                res.Activities.AddRange(mapper.Map<List<Component>, List<ActivityDTO>>(componentActivities));
                res.Activities.AddRange(mapper.Map<List<Library>, List<ActivityDTO>>(libraryActivities));

                return res;
            }
        }
    }
}
