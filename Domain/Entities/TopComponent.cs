using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TopComponent : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public Component Component { get; set; }
    }
}
