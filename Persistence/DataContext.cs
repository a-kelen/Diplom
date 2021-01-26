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
    public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DbSet<Component> Components { get; set; }
        public DbSet<Library> Libraries { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Prop> Props { get; set; }
        public DataContext(DbContextOptions options) : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Component>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Library>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");

            builder.Entity<User>().HasMany(t => t.Libraries)
                .WithOne(g => g.Owner)
                .HasForeignKey(g => g.UserId);

            builder.Entity<User>().HasMany(t => t.Components)
                .WithOne(g => g.Owner)
                .HasForeignKey(g => g.UserId);

            builder.Entity<User>().HasMany(t => t.OwnedComponents)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            builder.Entity<User>().HasMany(t => t.OwnedLibraries)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            builder.Entity<User>().HasMany(t => t.Followers)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);
            builder.Entity<User>().HasMany(t => t.Follows)
                .WithOne(g => g.Person)
                .HasForeignKey(g => g.PersonId).OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Component>().HasMany(t => t.Events)
                .WithOne(g => g.Component)
                .HasForeignKey(g => g.ComponentId);
            builder.Entity<Component>().HasMany(t => t.Owned)
                .WithOne(t => t.Component)
                .HasForeignKey(x => x.ComponentId).OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Component>().HasMany(t => t.Props)
                .WithOne(g => g.Component)
                .HasForeignKey(g => g.ComponentId);

            builder.Entity<Library>().HasMany(t => t.Components)
                .WithOne(g => g.Library)
                .HasForeignKey(g => g.LibraryId).OnDelete(DeleteBehavior.Restrict);


            builder.Entity<Library>().HasMany(t => t.Owned)
                .WithOne(t => t.Library)
                .HasForeignKey(x => x.LibraryId).OnDelete(DeleteBehavior.Restrict);


            //Seeding
            //new Seed(builder);
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
