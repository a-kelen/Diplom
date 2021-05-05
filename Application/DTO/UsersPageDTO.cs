using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UsersPageDTO
    {
        public int TotalUsers { get; set; }
        public int BlockedUsers { get; set; }
        public List<TableUserDTO> Users { get; set; }
    }
}
