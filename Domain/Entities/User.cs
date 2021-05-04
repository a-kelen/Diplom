using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string Avatar { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set;}
        public DateTime Created { get; set; }
        public Guid BlockId { get; set; }
        public UserBlock Block { get; set; }

        public List<Follower> Follows { get; set; }
        public List<Library> Libraries { get; set; }
        public List<Follower> Followers { get; set; }
        public List<Component> Components { get; set; }
        public List<OwnedLibrary> OwnedLibraries { get; set; }
        public List<OwnedComponent> OwnedComponents { get; set; }
        public List<UserReport> UserReports { get; set; }
        public List<Role> Roles { get; set; }
    }
}
