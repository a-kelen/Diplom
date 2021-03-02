using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.LibraryCQ.Queries
{
    public class LikedList
    {
        public class Query : IRequest<List<LibraryDTO>>
        {

        }
        public class Validator : AbstractValidator<Query>
        {
            public Validator()
            {

            }
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

                var ids = db.Likes.Where(x => x.Descriminator == LikeDescriminator.Library && x.UserId == id)
                    .Select(x => x.ElementId);
                var res = db.Libraries
                    .Where(x => ids.Contains(x.Id))
                    .ToList();

                return mapper.Map<List<Library>, List<LibraryDTO>>(res);
            }
        }
    }
}
