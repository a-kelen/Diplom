using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public Guid ComponentId { get; set; }
        public string Description { get; set; }
        public Component Component { get; set; }

    }
}
