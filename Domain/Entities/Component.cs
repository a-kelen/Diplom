using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Component : BaseTimeEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Status { get; set; }
        public string FileName { get; set; }
        public Guid UserId { get; set; }
        public User Owner { get; set; }
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
        public List<Prop> Props { get; set; }
        public List<Event> Events { get; set; }
        public List<OwnedComponent> Owned { get; set; }
    }
}
