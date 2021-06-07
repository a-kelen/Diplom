using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Label : BaseEntity
    {
        public string Name { get; set; }
        public List<Library> Libraries { get; set; }
        public List<Component> Components { get; set; }
    }
}
