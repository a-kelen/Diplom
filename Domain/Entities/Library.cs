using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Library : BaseTimeEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public Guid UserId { get; set; }
        public User Owner { get; set; }
        public List<Component> Components { get; set; }
        public List<OwnedLibrary> Owned { get; set; }

    }
}
