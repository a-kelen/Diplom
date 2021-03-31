using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.LibraryCQ.Data
{
    public class LibraryItemLikeAction : IMappingAction<Library, LibraryDTO>
    {
        DataContext db;
        iUserAccessor userAccessor;

        public LibraryItemLikeAction(DataContext context, iUserAccessor accessor)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            userAccessor = accessor;
        }

        public void Process(Library source, LibraryDTO destination, ResolutionContext context)
        {
            var id = userAccessor.GetId();
            destination.Likes = db.Likes.Where(x => x.ElementId == destination.Id).Count();
            destination.Liked = db.Likes.Where(x => x.ElementId == destination.Id && x.UserId == id).Count() > 0;
        }
    }
}
