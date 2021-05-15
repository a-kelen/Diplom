using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class HistoryItem : BaseTimeEntity
    {
        public HistoryAction Action { get; set; }
        public HistoryType Type { get; set; }
        public Guid ElementId { get; set; }
        public User User { get; set; }
        public Guid UserId { get; set; }

    }
}
