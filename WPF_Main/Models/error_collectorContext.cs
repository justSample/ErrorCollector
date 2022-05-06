using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WPF_Main.Models
{
    public partial class error_collectorContext : DbContext
    {
        public error_collectorContext()
        {
        }

        public error_collectorContext(DbContextOptions<error_collectorContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Errors> Errors { get; set; }
        public virtual DbSet<Instructions> Instructions { get; set; }
        public virtual DbSet<Programs> Programs { get; set; }
        public virtual DbSet<Steps> Steps { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();


            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("server=127.0.0.1;user=root;password=root;database=error_collector;Charset=utf8;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Errors>(entity =>
            {
                entity.ToTable("errors");

                entity.HasIndex(e => e.IdProgram)
                    .HasName("fkey_id_prog_idx");

                entity.HasIndex(e => e.IdUserCreated)
                    .HasName("fkey_id_user_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Cause)
                    .HasColumnName("cause")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Comment)
                    .HasColumnName("comment")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdProgram)
                    .HasColumnName("id_program")
                    .HasColumnType("int(11)");

                entity.Property(e => e.IdUserCreated)
                    .HasColumnName("id_user_created")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Images).HasColumnName("images");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Solution)
                    .HasColumnName("solution")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdProgramNavigation)
                    .WithMany(p => p.Errors)
                    .HasForeignKey(d => d.IdProgram)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkey_id_prog");

                entity.HasOne(d => d.IdUserCreatedNavigation)
                    .WithMany(p => p.Errors)
                    .HasForeignKey(d => d.IdUserCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkey_id_user");
            });

            modelBuilder.Entity<Instructions>(entity =>
            {
                entity.ToTable("instructions");

                entity.HasIndex(e => e.IdUserCreated)
                    .HasName("fkey_id_user_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateChange)
                    .HasColumnName("date_change")
                    .HasColumnType("datetime");

                entity.Property(e => e.DateCreated)
                    .HasColumnName("date_created")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdUserCreated)
                    .HasColumnName("id_user_created")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.HasOne(d => d.IdUserCreatedNavigation)
                    .WithMany(p => p.Instructions)
                    .HasForeignKey(d => d.IdUserCreated)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkey_id_user_crtd");
            });

            modelBuilder.Entity<Programs>(entity =>
            {
                entity.ToTable("programs");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnName("name")
                    .HasColumnType("varchar(120)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            modelBuilder.Entity<Steps>(entity =>
            {
                entity.ToTable("steps");

                entity.HasIndex(e => e.IdInstructions)
                    .HasName("fkey_id_inst_idx");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ActionDescription)
                    .HasColumnName("action_description")
                    .HasColumnType("mediumtext")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.Header)
                    .HasColumnName("header")
                    .HasColumnType("varchar(300)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");

                entity.Property(e => e.IdInstructions)
                    .HasColumnName("id_instructions")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Images).HasColumnName("images");

                entity.Property(e => e.Number)
                    .HasColumnName("number")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.IdInstructionsNavigation)
                    .WithMany(p => p.Steps)
                    .HasForeignKey(d => d.IdInstructions)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fkey_id_inst");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasColumnType("varchar(90)")
                    .HasCharSet("utf8")
                    .HasCollation("utf8_general_ci");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
