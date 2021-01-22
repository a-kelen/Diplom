using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ComponentReport : BaseTimeEntity
    {
        public Guid ComponentId { get; set; }
        public Component Component { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
