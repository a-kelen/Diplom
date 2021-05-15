using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistence
{
    public static class Relationships
    {
        private static ModelBuilder builder;
        public static void Init(ModelBuilder _builder)
        {
            builder = _builder;
            timeStamps();
            userRelationships();
            componentRelationships();
            libraryRelationships();
        }

        static void timeStamps()
        {
            builder.Entity<User>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Component>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Library>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<OwnedLibrary>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<OwnedComponent>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<Follower>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<UserReport>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<LibraryReport>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<ComponentReport>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
            builder.Entity<HistoryItem>()
                .Property(b => b.Created)
                .HasDefaultValueSql("getdate()");
        }
        static void userRelationships()
        {
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

            builder.Entity<User>().HasMany(t => t.Follows)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);

            builder.Entity<User>().HasMany(t => t.Followers)
                .WithOne(g => g.Person)
                .HasForeignKey(g => g.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(t => t.UserReports)
                .WithOne(g => g.Person)
                .HasForeignKey(g => g.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasOne(t => t.Block)
                .WithOne(g => g.Person)
                .HasForeignKey<UserBlock>(k => k.PersonId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>().HasMany(t => t.HistoryItems)
               .WithOne(g => g.User)
               .HasForeignKey(g => g.UserId);

        }
        static void componentRelationships()
        {
            builder.Entity<Component>().HasMany(t => t.Events)
               .WithOne(g => g.Component)
               .HasForeignKey(g => g.ComponentId);

            builder.Entity<Component>().HasMany(t => t.Owned)
                .WithOne(t => t.Component)
                .HasForeignKey(x => x.ComponentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Component>().HasMany(t => t.Props)
                .WithOne(g => g.Component)
                .HasForeignKey(g => g.ComponentId);

            builder.Entity<Component>().HasMany(t => t.Slots)
               .WithOne(g => g.Component)
               .HasForeignKey(g => g.ComponentId);

            

            builder.Entity<Component>().HasMany(t => t.Reports)
                .WithOne(g => g.Component)
                .HasForeignKey(g => g.ComponentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Component>().HasOne(t => t.Block)
                .WithOne(g => g.Component)
                .HasForeignKey<ComponentBlock>(k => k.ComponentId)
                .OnDelete(DeleteBehavior.Restrict); ;
        }
        static void libraryRelationships()
        {
            builder.Entity<Library>().HasMany(t => t.Components)
                .WithOne(g => g.Library)
                .HasForeignKey(g => g.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Library>().HasMany(t => t.Owned)
                .WithOne(t => t.Library)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Library>().HasMany(t => t.Reports)
                .WithOne(t => t.Library)
                .HasForeignKey(x => x.LibraryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Library>().HasOne(t => t.Block)
                .WithOne(g => g.Library)
                .HasForeignKey<LibraryBlock>(k => k.LibraryId)
                .OnDelete(DeleteBehavior.Restrict); ;
        }

    }
}
