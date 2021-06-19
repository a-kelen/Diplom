using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Persistence
{
    public class DataContext : IdentityDbContext<User, Role, Guid>
    {
        public DbSet<ComponentReport> ComponentReports { get; set; }
        public DbSet<ComponentBlock> ComponentBlocks { get; set; }
        public DbSet<OwnedComponent> OwnedComponents { get; set; }
        public DbSet<TopComponent> TopComponents { get; set; }
        public DbSet<LibraryReport> LibraryReports { get; set; }
        public DbSet<OwnedLibrary> OwnedLibraries { get; set; }
        public DbSet<LibraryBlock> LibraryBlocks { get; set; }
        public DbSet<TopLibrary> TopLibraries { get; set; }
        public DbSet<HistoryItem> HistoryItems { get; set; }
        public DbSet<UserReport> UserReports { get; set; }
        public DbSet<UserBlock> UserBlocks { get; set; }
        public DbSet<TopUser> TopUsers { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<Follower>  Followers{ get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Label> Labels { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Prop> Props { get; set; }
        public DbSet<Slot> Slots { get; set; }


        public DataContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
            if(false)
            {
                Database.EnsureDeleted();
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
    }
}
