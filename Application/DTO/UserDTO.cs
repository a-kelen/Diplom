using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public bool HasAvatar { get; set; }
        public string Username { get; set; }
        public int LibraryCount{ get; set; }
        public int FollowerCount { get; set; }
        public int ComponentCount { get; set; }

    }
}
