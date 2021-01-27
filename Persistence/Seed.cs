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

        private static void UserSeed()
        {
            string ADMIN_ID = "D2429ACD-E887-47F8-8AD2-6502E05C9068";
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
            PasswordHasher<User> ph = new PasswordHasher<User>();
            user.PasswordHash = ph.HashPassword(user, "_123123Aa");
            builder.Entity<User>().HasData(user);
        }
    }
}
