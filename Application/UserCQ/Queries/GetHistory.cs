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

                var history = await db.HistoryItems.Where(x => x.UserId == userId).ToListAsync();       
                return mapper.Map <List<HistoryItem>, List<HistoryItemDTO>>(history);
            }
        }
    }
}
