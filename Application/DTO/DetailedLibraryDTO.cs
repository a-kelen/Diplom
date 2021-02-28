using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class DetailedLibraryDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public List<ComponentDTO> Components { get; set; }
    }
}
