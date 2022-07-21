using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UserManagement.Models
{
    public partial class UserManagementContext : DbContext
    {
        public UserManagementContext()
        {
        }

        public UserManagementContext(DbContextOptions<UserManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Title> Titles { get; set; } = null!;
        public virtual DbSet<WebUser> WebUsers { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ConnectionStrings:UserManagementDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Title>(entity =>
            {
                entity.ToTable("title");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<WebUser>(entity =>
            {
                entity.ToTable("web_user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Avatar)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("avatar");

                entity.Property(e => e.Company)
                    .HasMaxLength(10)
                    .HasColumnName("company")
                    .HasDefaultValueSql("('ROSEN')");

                entity.Property(e => e.DateOfBirth)
                    .HasColumnType("date")
                    .HasColumnName("date_of_birth");

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.FirstName)
                    .HasMaxLength(100)
                    .HasColumnName("first_name");

                entity.Property(e => e.Gender).HasColumnName("gender");

                entity.Property(e => e.LastName)
                    .HasMaxLength(100)
                    .HasColumnName("last_name");

                entity.Property(e => e.Title).HasColumnName("title");

                entity.HasOne(d => d.TitleNavigation)
                    .WithMany(p => p.WebUsers)
                    .HasForeignKey(d => d.Title)
                    .HasConstraintName("FK__web_user__title__1367E606");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
