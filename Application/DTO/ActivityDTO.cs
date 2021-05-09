using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ActivityDTO
    {
        public string Author { get; set; }
        public bool HasAvatar { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
