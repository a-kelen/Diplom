using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ComponentReportsPageDTO
    {
        public int PageSize { get; set; }
        public int CurrentPage { get; set; }
        public int TotalReports { get; set; }
        public int AdmittedReports { get; set; }
        public List<ComponentReportDTO> Reports { get; set; }
    }
}
