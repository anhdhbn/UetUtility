using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace UetGrade.Models
{
    public partial class UetContext : DbContext
    {
        public UetContext()
        {
        }

        public UetContext(DbContextOptions<UetContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ConnectorSg> ConnectorSg { get; set; }
        public virtual DbSet<Grade> Grade { get; set; }
        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<SystemConfig> SystemConfig { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("data source=den1.mssql8.gear.host;Database=UetGrade;initial catalog=UetGrade;persist security info=True;user id=uetgrade;password=Honganh99^^;multipleactiveresultsets=True;");
                //optionsBuilder.UseSqlServer("data source=anhdh.c2nlby2h44e0.ap-southeast-1.rds.amazonaws.com;initial catalog=UetGrade;persist security info=True;user id=anhdh;password=Honganh99;multipleactiveresultsets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.0-rtm-35687");

            modelBuilder.Entity<ConnectorSg>(entity =>
            {
                entity.ToTable("ConnectorSG");

                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Grade)
                    .WithMany(p => p.ConnectorSg)
                    .HasForeignKey(d => d.GradeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeStudent_Grade");

                entity.HasOne(d => d.Student)
                    .WithMany(p => p.ConnectorSg)
                    .HasForeignKey(d => d.StudentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GradeStudent_Student");
            });

            modelBuilder.Entity<Grade>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Time).HasColumnType("datetime");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.MessageId).IsRequired();
            });

            modelBuilder.Entity<SystemConfig>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.CurrentTimeGrade).HasColumnType("datetime");
            });
        }
    }
}
