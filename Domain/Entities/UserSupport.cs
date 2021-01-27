using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserSupport : BaseTimeEntity
    {
        public User User { get; set; }
        public Guid UserId { get; set; }
        public User Person { get; set; }
        public Guid PersonId { get; set; }
    }
}
