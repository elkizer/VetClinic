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
        public DbSet<PersonAddress> PersonAddresses { get; set; }
        public DbSet<PersonPhone> PersonPhones { get; set; }
        public DbSet<PersonEmail> PersonEmails { get; set; }
        public DbSet<ClientAnimal> ClientAnimals { get; set; }
        public DbSet<EmployeeType> EmployeeTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientAnimal>()
        .HasKey(ca => new { ca.ClientId, ca.AnimalId });
            modelBuilder.Entity<ClientAnimal>()
                .HasOne(ca => ca.Client)
                .WithMany(c => c.ClientAnimals)
                .HasForeignKey(ca => ca.ClientId);
            modelBuilder.Entity<ClientAnimal>()
                .HasOne(ca => ca.Animal)
                .WithMany(c => c.ClientAnimals)
                .HasForeignKey(ca => ca.AnimalId);

            modelBuilder.Entity<Animal>().ToTable("Animal");
            modelBuilder.Entity<Species>().ToTable("Species");
            modelBuilder.Entity<Appointment>().ToTable("Appointment");
            modelBuilder.Entity<Client>().ToTable("Client");
            modelBuilder.Entity<Employee>().ToTable("Employee");
            modelBuilder.Entity<Person>().ToTable("Person");
            modelBuilder.Entity<PersonAddress>().ToTable("PersonAddress");
            modelBuilder.Entity<PersonPhone>().ToTable("PersonPhone");
            modelBuilder.Entity<PersonEmail>().ToTable("PersonEmail");
            modelBuilder.Entity<ClientAnimal>().ToTable("ClientAnimal");
            modelBuilder.Entity<EmployeeType>().ToTable("EmployeeType");


        }
    }
}
