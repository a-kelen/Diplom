using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserReport : BaseTimeEntity
    {
        public ReportStatus Status { get; set; }
        public string Content { get; set; }
        public Guid PersonId { get; set; }
        public User Person { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        
    }
}
