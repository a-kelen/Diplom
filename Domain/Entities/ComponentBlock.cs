﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ComponentBlock : BaseEntity
    {
        public Guid ComponentId { get; set; }
        public Component Component { get; set; }

        public Guid UserId { get; set; }
        public User User { get; set; }

    }
}
