using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class HistoryElement : BaseTimeEntity
    {
        public string Action { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
