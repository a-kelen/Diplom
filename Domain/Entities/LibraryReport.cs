using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LibraryReport : BaseTimeEntity
    {
        public Library Library { get; set; }
        public Guid LibraryId { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
