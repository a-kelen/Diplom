﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ComponentDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Format { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
    }
}
