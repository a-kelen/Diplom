using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Pic : BaseEntity
    {
        public string Path { get; set; }
        public Guid ComponentId { get; set; }
        public Component Component { get; set; }
    }
}
