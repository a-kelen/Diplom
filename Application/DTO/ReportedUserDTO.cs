﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class ReportedUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Status { get; set; }
        public string Username { get; set; }
        public int ReportsCount { get; set; }
    }
}
