using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CollegeManagement.Models
{
    public partial class collegemanagementContext : DbContext
    {
        public collegemanagementContext()
        {
        }

        public collegemanagementContext(DbContextOptions<collegemanagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Staffdetail> Staffdetails { get; set; } = null!;
        public virtual DbSet<Studentdetail> Studentdetails { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;port=3306;user=root;password=Eswar@001;database=collegemanagement", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.28-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Staffdetail>(entity =>
            {
                entity.HasKey(e => e.Staffid)
                    .HasName("PRIMARY");

                entity.ToTable("staffdetails");

                entity.Property(e => e.Staffid)
                    .ValueGeneratedNever()
                    .HasColumnName("staffid");

                entity.Property(e => e.Address)
                    .HasMaxLength(45)
                    .HasColumnName("address");

                entity.Property(e => e.Designation)
                    .HasMaxLength(45)
                    .HasColumnName("designation");

                entity.Property(e => e.Doj).HasColumnName("doj");

                entity.Property(e => e.Staffname)
                    .HasMaxLength(45)
                    .HasColumnName("staffname");

                entity.Property(e => e.Stream)
                    .HasMaxLength(45)
                    .HasColumnName("stream");
            });

            modelBuilder.Entity<Studentdetail>(entity =>
            {
                entity.ToTable("studentdetails");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(45)
                    .HasColumnName("address");

                entity.Property(e => e.Batch).HasColumnName("batch");

                entity.Property(e => e.Department)
                    .HasMaxLength(45)
                    .HasColumnName("department");

                entity.Property(e => e.Phoneno)
                    .HasMaxLength(45)
                    .HasColumnName("phoneno");

                entity.Property(e => e.Studentname)
                    .HasMaxLength(45)
                    .HasColumnName("studentname");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Email)
                    .HasMaxLength(45)
                    .HasColumnName("email");

                entity.Property(e => e.Isadmin).HasColumnName("isadmin");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Password)
                    .HasMaxLength(45)
                    .HasColumnName("password");
                entity.Property(e => e.Status).HasColumnName("status");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
