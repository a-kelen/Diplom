﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<ComponentReport> ComponentReports { get; set; }
        public DbSet<LibraryReport> LibraryReports { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Prop> Props { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {
            if(false)
            {
                //Database.EnsureDeleted();
                Database.EnsureCreated();
            }
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Relationships
            Relationships.Init(builder);

            //Seeding
            Seed.Init(builder);
        }
        //public override int SaveChanges()
        //{
        //    var entries = ChangeTracker
        //        .Entries()
        //        .Where(e => e.Entity is BaseTimeEntity && (
        //                e.State == EntityState.Added
        //                || e.State == EntityState.Modified));

        //    foreach (var entityEntry in entries)
        //    {

        //        if (entityEntry.State == EntityState.Added)
        //        {
        //            ((BaseTimeEntity)entityEntry.Entity).Created = DateTime.Now;
        //        }
        //    }

        //    return base.SaveChanges();
        //}
    }
}
