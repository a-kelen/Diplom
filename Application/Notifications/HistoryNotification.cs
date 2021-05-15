using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Notifications
{
    public class HistoryNotification : INotification
    {
        public HistoryAction Action { get; set; }
        public Guid ElementId { get; set; }
        public HistoryType Type { get; set; }
    }
}
