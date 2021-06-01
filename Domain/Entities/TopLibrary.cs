using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TopLibrary : BaseEntity
    {
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
