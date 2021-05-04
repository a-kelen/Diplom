using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserReportsPageDTO
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public List<UserReportDTO> Reports { get; set; }
    }
}
