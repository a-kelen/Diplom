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
            string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
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
            user.PasswordHash = ph.HashPassword(user, "123123");
            this.builder.Entity<User>().HasData(user);
        }
    }
}
