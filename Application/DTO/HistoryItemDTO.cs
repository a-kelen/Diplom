using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class HistoryItemDTO
    {
        public Guid Id { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public string ElementName { get; set; }
        public string Action { get; set; }
    }
}
