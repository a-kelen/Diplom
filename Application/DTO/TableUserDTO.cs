using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class TableUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public int TotalReports { get; set; }
        public int AdmittedReports { get; set; }
    }
}
