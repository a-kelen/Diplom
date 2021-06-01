using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TopUser : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
