using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserReportDTO
    {
        public Guid ReportedUserId { get; set; }
        public string Status { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }

    }
}
