using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ApiPeliculas.Domain.Entities;
using ApiPeliculas.Infraestructure.Data;

#nullable disable

namespace ApiPeliculas.Infraestructure.Data
{
    public partial class peliculaspContext : DbContext
    {
        public peliculaspContext()
        {
        }

        public peliculaspContext(DbContextOptions<peliculaspContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Pelitabla> Pelitablas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Pelitabla>(entity =>
            {
                entity.ToTable("Pelitabla");

                entity.Property(e => e.Director)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Fecha)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Genero)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Rating).HasColumnType("decimal(3, 1)");

                entity.Property(e => e.Titulo)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}