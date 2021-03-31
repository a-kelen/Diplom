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
    public class ComponentItemLikeAction : IMappingAction<Component, ComponentDTO>
    {
        DataContext db;
        iUserAccessor userAccessor;

        public ComponentItemLikeAction(DataContext context, iUserAccessor accessor)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            userAccessor = accessor;
        }

        public void Process(Component source, ComponentDTO destination, ResolutionContext context)
        {
            var id = userAccessor.GetId();
            destination.Likes = db.Likes.Where(x => x.ElementId == destination.Id).Count();
            destination.Liked = db.Likes.Where(x => x.ElementId == destination.Id && x.UserId == id).Count() > 0;

        }
    }
}
