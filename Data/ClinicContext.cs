using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Data
{
    public class ClinicContext : DbContext
    {
        public ClinicContext(DbContextOptions<ClinicContext> options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().ToTable("Animal");
            modelBuilder.Entity<Species>().ToTable("Species");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Person>().ToTable("Person");
        }
    }
}
