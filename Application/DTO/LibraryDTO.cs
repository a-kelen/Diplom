using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class LibraryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public int ComponentsCount { get; set; }
    }
}
