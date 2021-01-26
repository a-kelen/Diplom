using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public class Seed
    {
        private ModelBuilder builder;
        public Seed(ModelBuilder builder)
        {
            this.builder = builder;
            this.UserSeed();
        }

        private void UserSeed()
        {
            string ADMIN_ID = "D2429ACD-E887-47F8-8AD2-6502E05C9068";
            var user = new User
            {
                Id = new Guid(ADMIN_ID),
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                Firstname = "Admin",
                Lastname = "Adminenko",
                UserName = "admin"
            };
            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "_123123Aa");
            this.builder.Entity<User>().HasData(user);
        }
    }
}
