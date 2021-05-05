using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ReportedComponentsPageDTO
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalReports { get; set; }
        public List<ReportedComponentDTO> Components { get; set; }
    }
}
