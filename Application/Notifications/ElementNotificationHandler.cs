using Application.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Persistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notifications
{
    public class ElementNotificationHandler : INotificationHandler<HistoryNotification>
    {
        DataContext db;
        iUserAccessor userAccessor;
        public ElementNotificationHandler(DataContext db, iUserAccessor userAccessor)
        {
            this.db = db;
            this.userAccessor = userAccessor;
        }
        
        public async Task Handle(HistoryNotification notification, CancellationToken cancellationToken)
        {

            db.HistoryItems.Add(new HistoryItem { Action = notification.Action, ElementId = notification.ElementId, Type = notification.Type, UserId = userAccessor.GetId()});
            await db.SaveChangesAsync();
        }


    }
    public class Validator : AbstractValidator<HistoryNotification>
    {
        public Validator()
        {
            RuleFor(x => x.Type).NotEmpty();
            RuleFor(x => x.Action).NotEmpty();
            RuleFor(x => x.ElementId).NotEmpty();
        }
    }
}
