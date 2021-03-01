using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("OwnedLibrary")]
    public class OwnedLibrary : BaseTimeEntity
    {
        public Guid LibraryId { get; set; }
        public Library Library{ get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
