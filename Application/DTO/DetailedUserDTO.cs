using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTO
{
    public class DetailedUserDTO
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Image { get; set; }
        public bool Followed { get; set; }
        public string Username { get; set; }
        public int FollowersCount { get; set; }
        public FileContentResult Avatar { get; set; }
        public List<LibraryDTO> Libraries { get; set; }
        public List<ComponentDTO> Components { get; set; }
    }
}
