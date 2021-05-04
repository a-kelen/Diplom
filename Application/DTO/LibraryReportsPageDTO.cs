using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class LibraryReportsPageDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<LibraryReportDTO> Reports { get; set; }
    }
}
