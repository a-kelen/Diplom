using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ComponentReportsPageDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<ComponentReportDTO> Reports { get; set; }
    }
}
