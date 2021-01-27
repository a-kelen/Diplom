using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserReportDTO
    {
        public Guid ReportedUserId { get; set; }
        public bool Status { get; set; }

    }
}
