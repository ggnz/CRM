using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OpenSaludSecurity.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpenSaludSecurity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Request> Request { get; set; }
        public DbSet<Clinica> Clinica { get; set; }
        public DbSet<Cita> Citas { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Calificacion> Calificaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Calificacion>().ToTable("Calificacion");
            modelBuilder.Entity<Clinica>().ToTable("Clinica");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Medico>().ToTable("Medico");
            modelBuilder.Entity<Cita>().ToTable("Cita");
            base.OnModelCreating(modelBuilder);

        }
    }
}
