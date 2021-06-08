﻿using Application.DTO;
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

namespace Application.ComponentCQ.Queries
{
    public class LikedList
    {
        public class Query : IRequest<List<ComponentDTO>>
        {

        }
       
        public class Handler : IRequestHandler<Query, List<ComponentDTO>>
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

            public async Task<List<ComponentDTO>> Handle(Query request, CancellationToken cancellationToken)
            {
                var id = userAccesor.GetId();

                var ids = db.Likes.Where(x => x.Descriminator == LikeDescriminator.Component && x.UserId == id)
                    .Select(x => x.ElementId);
                var res = db.Components
                    .Include(x => x.Labels)
                    .Where(x => ids.Contains(x.Id))
                    .ToList();

                return mapper.Map <List<Component>, List<ComponentDTO>> (res);
            }
        }
    }
}
