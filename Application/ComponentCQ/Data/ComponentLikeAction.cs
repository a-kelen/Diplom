using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.ComponentCQ.Data
{
    public class ComponentLikeAction : IMappingAction<Component, DetailedComponentDTO>
    {
        DataContext db;
        iUserAccessor userAccessor;

        public ComponentLikeAction(DataContext context, iUserAccessor accessor)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            userAccessor = accessor;
        }

        public void Process(Component source, DetailedComponentDTO destination, ResolutionContext context)
        {
            var id = userAccessor.GetId();
            destination.Liked = db.Likes.Where(x => x.ElementId == destination.Id && x.UserId == id).Count() > 0;
            destination.Owned = db.OwnedComponents.Any(x => x.UserId == id && x.ComponentId == destination.Id);

        }
    }
}
