using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Component : BaseTimeEntity
    {
        public User Owner { get; set; }
        public Guid? UserId { get; set; }
        public string Name { get; set; }
        public bool Status { get; set; }
        public string File { get; set; }
        public bool Deleted { get; set; }
        public Guid? LibraryId { get; set; }
        public Library Library { get; set; }
        public string Description { get; set; }

        public List<File> Files { get; set; }
        public List<Prop> Props { get; set; }
        public List<Slot> Slots { get; set; }
        public List<Event> Events { get; set; }
        public List<OwnedComponent> Owned { get; set; }
        public List<ComponentReport> Reports { get; set; }

    }
}
