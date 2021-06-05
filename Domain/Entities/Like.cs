using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid ElementId { get; set; }
        public LikeDescriminator Descriminator { get; set; }
    }
    public enum LikeDescriminator
    {
        Library,
        Component
    }
}
