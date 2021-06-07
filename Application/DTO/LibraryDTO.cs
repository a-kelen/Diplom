﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class LibraryDTO
    {
        public Guid Id { get; set; }
        public int Likes { get; set; }
        public bool Liked { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public bool HasAvatar { get; set; }
        public DateTime Created { get; set; }
        public int ComponentsCount { get; set; }
        public List<string> Labels { get; set; }

    }
}
