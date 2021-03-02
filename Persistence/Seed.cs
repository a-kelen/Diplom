using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class Seed
    {
        private static ModelBuilder builder;
        public static void Init(ModelBuilder _builder)
        {
            builder = _builder;
            UserSeed();
        }
        static string ADMIN_ID = "D2429ACD-E887-47F8-8AD2-6502E05C9068";
        static string USER1_ID = "D350AFFF-86B3-449B-BE6C-E87394D5A629";
        private static void UserSeed()
        {
            
            var user = new User
            {
                Id = new Guid(ADMIN_ID),
                Email = "admin@gmail.com",
                NormalizedEmail = "ADMIN@GMAIL.COM",
                Created = DateTime.Now,
                EmailConfirmed = true,
                Firstname = "Admin",
                Lastname = "Adminenko",
                UserName = "admin"
            };
            var user1 = new User
            {
                Id = new Guid(USER1_ID),
                Email = "user1@gmail.com",
                NormalizedEmail = "USER1@GMAIL.COM",
                Created = DateTime.Now,
                EmailConfirmed = true,
                Firstname = "User1",
                Lastname = "Userenko",
                UserName = "user1"
            };
            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "_123123Aa");
            builder.Entity<User>().HasData(user);

            user1.PasswordHash = ph.HashPassword(user1, "_123123Aa");
            builder.Entity<User>().HasData(user1);
        }

    }
}
