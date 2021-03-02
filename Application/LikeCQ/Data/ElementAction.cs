using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.LikeCQ.Data
{
    public class ElementAction : IMappingAction<Like, LikeDTO>
    {
        DataContext db;
        IMapper mapper;

        public ElementAction(DataContext context, IMapper mapper)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            this.mapper = mapper;
        }

        public void Process(Like source, LikeDTO destination, ResolutionContext context)
        {
            if(source.Descriminator == LikeDescriminator.Library)
            {
                destination.Library = mapper.Map<Library, LibraryDTO>(db.Libraries.FirstOrDefault(x => x.Id == source.ElementId));
            }
            else
            {
                destination.Component = mapper.Map<Component, ComponentDTO>(db.Components.FirstOrDefault(x => x.Id == source.ElementId));
            }

        }
    }
}
