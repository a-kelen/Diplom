using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class CurrentUserDTO
    {
        public string Name { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Token { get; set; }
        public bool HasAvatar { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public string Username { get; set; }

    }
}
