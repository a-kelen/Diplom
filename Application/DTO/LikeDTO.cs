using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class LikeDTO
    {
        public Guid Id { get; set; }
        public ComponentDTO Component { get; set; }
        public LibraryDTO Library { get; set; }
        public DateTime DateTime { get; set; }
    }
}
