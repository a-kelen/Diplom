using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LibraryReport : BaseTimeEntity
    {
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
