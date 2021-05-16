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
    public class GetHistory
    {
        public class Query : IRequest<List<HistoryItemDTO>>
        {

        }
    
        public class Handler : IRequestHandler<Query, List<HistoryItemDTO>>
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

            public async Task<List<HistoryItemDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = userAccesor.GetId();
                var history = await db.HistoryItems
                    .Where(x => x.UserId == userId)
                    .ToListAsync();

                var historyIds = db.HistoryItems
                    .Where(x => x.UserId == userId)
                    .Select(x => x.ElementId);

                var components = await db.Components
                    .Include(x => x.Owner)
                    .Where(x => historyIds.Contains(x.Id))
                    .Select(x => new {
                       Name = $"@{x.Owner.UserName}/{x.Name}",
                       Id = x.Id
                    })
                    .ToListAsync();

                var libraries = await db.Libraries
                    .Include(x => x.Owner)
                    .Where(x => historyIds.Contains(x.Id))
                    .Select(x => new {
                        Name = $"@{x.Owner.UserName}/{x.Name}",
                        Id = x.Id
                    })
                    .ToListAsync();



                var res = mapper.Map<List<HistoryItem>, List<HistoryItemDTO>>(history);

                for(int i = 0; i < history.Count; i++)
                {
                    if(history[i].Type == HistoryType.Component)
                    {
                        res[i].ElementName = components.First(x => x.Id == history[i].ElementId).Name;
                    } else
                    {
                        res[i].ElementName = libraries.First(x => x.Id == history[i].ElementId).Name;
                    }
                }

                return  res;
            }
        }
    }
}
