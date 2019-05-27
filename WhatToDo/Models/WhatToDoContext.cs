using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WhatToDo.Models
{
    public partial class WhatToDoContext : DbContext
    {
        public WhatToDoContext()
        {
        }

        public WhatToDoContext(DbContextOptions<WhatToDoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Event> Event { get; set; }
        public virtual DbSet<Place> Place { get; set; }
        public virtual DbSet<PlaceEventConnection> PlaceEventConnection { get; set; }
        public virtual DbSet<Preference> Preference { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<Wishlist> Wishlist { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                throw new Exception("DB context is not configurated");                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>(entity =>
            {
                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Place>(entity =>
            {
                entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<PlaceEventConnection>(entity =>
            {
                entity.HasKey(e => e.Peid);

                entity.Property(e => e.Peid).HasColumnName("PEID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.PlaceEventConnection)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.PlaceEventConnection)
                    .HasForeignKey(d => d.PlaceId);
            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.HasKey(e => e.PrefId);

                entity.Property(e => e.PrefId).HasColumnName("PrefID");

                entity.Property(e => e.IsLike).HasDefaultValueSql("((0))");

                entity.Property(e => e.PlaceId).HasColumnName("PlaceID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Place)
                    .WithMany(p => p.Preference)
                    .HasForeignKey(d => d.PlaceId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Preference)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(30);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<Wishlist>(entity =>
            {
                entity.Property(e => e.WishlistId).HasColumnName("WishlistID");

                entity.Property(e => e.EventId).HasColumnName("EventID");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.HasOne(d => d.Event)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.EventId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Wishlist)
                    .HasForeignKey(d => d.UserId);
            });
        }
    }
}
