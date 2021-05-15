using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class FolderDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<string> Files { get; set; }
    }
}
