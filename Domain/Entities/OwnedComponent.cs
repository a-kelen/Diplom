using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    [Table("OwnedComponent")]
    public class OwnedComponent : BaseTimeEntity
    {
        public Component Component { get; set; }
        public Guid ComponentId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
