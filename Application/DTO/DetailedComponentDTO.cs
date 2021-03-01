﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class DetailedComponentDTO
    {
        public Guid Id { get; set; }
        public bool Liked { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Format { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<PropDTO> Props { get; set; }
        public List<EventDTO> Events { get; set; }
    }
}