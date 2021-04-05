using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Library : BaseTimeEntity
    {
        public User Owner { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string File { get; set; }
        public bool Deleted { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }

        public List<OwnedLibrary> Owned { get; set; }
        public List<Component> Components { get; set; }
        public List<LibraryReport> Reports { get; set; }
    }
}
