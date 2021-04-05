using Application.DTO;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.UserCQ.Data
{
    public class FollowedAction : IMappingAction<User, DetailedUserDTO>
    {
        DataContext db;
        iUserAccessor userAccessor;

        public FollowedAction(DataContext context, iUserAccessor accessor)
        {
            db = context ?? throw new ArgumentNullException(nameof(context));
            userAccessor = accessor;
        }

        public void Process(User source, DetailedUserDTO destination, ResolutionContext context)
        {
            var id = userAccessor.GetId();
            destination.Followed = db.Followers.Any(x => x.UserId == id && x.PersonId == source.Id);
        }
    }
}
