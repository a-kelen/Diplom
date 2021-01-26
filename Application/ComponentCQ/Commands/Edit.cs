﻿using Application.DTO;
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

namespace Application.ComponentCQ.Commands
{
    public class Edit
    {
        public class Command : IRequest<ComponentDTO>
        {
            public Guid Id { get; set; }
            public string Description { get; set; }
            public List<EventVM> Events { get; set; }
            public List<PropVM> Props { get; set; }

        }
        public class Validator : AbstractValidator<Command>
        {
            public Validator()
            {
                RuleFor(x => x.Description).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, ComponentDTO>
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

            public async Task<ComponentDTO> Handle(Command request, CancellationToken cancellationToken)
            {
                Component component = await db.Components.FirstOrDefaultAsync(x => x.Id == request.Id);
                if (component == null)
                    throw new RestException(HttpStatusCode.NotFound, new { Component = "Not found" });
                db.Entry(component).CurrentValues.SetValues(request);
                await db.SaveChangesAsync();
                return mapper.Map<Component, ComponentDTO>(component);
            }
        }
    }
}