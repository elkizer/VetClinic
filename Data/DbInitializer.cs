using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ClinicContext context)
        {
            context.Database.EnsureCreated();

            // Populate species
            if (context.Species.Any())
            {
                return;
            }

            var species = new Species[]
            {
                new Species{Code="CANINE", Name = "Canine", Description = "Canine"},
                new Species{Code = "FELINE", Name = "Feline", Description = "Feline"}
            };
            foreach (Species s in species)
            {
                context.Species.Add(s);
            }
            context.SaveChanges();

            // Populate EmployeeTypes
            var employeeTypes = new EmployeeType[]
            {
                new EmployeeType{Code = "VET", Name = "Veterinarian", Description = "Veterinarian"},
                new EmployeeType{Code = "TECH", Name = "Technician", Description = "Technician"},
                new EmployeeType{Code = "ADMIN", Name = "Administration", Description = "Administration"}
            };
            foreach (EmployeeType ea in employeeTypes)
            {
                context.EmployeeTypes.Add(ea);
            }
            context.SaveChanges();

            // Populate Employees
            var employee = new Employee[]
            {
                new Employee{FirstName = "Jeanne", LastName = "Sparks", EmployeeTypeId = employeeTypes.Single(ea => ea.Code == "VET").EmployeeTypeId, HireDate = DateTime.Parse("2018-08-01")},
                new Employee{FirstName = "Jenny", LastName = "Palmer", EmployeeTypeId = employeeTypes.Single(ea => ea.Code == "TECH").EmployeeTypeId, HireDate = DateTime.Parse("2018-08-01")}
            };
            foreach (Employee e in employee)
            {
                context.Employees.Add(e);
            }
            context.SaveChanges();

            // Populate clients
            var clients = new Client[]
            {
                new Client{ FirstName = "Chelsea", LastName = "Bridges"},
                new Client { FirstName = "Robin", LastName = "Kizer"},
                new Client{ FirstName = "John", LastName = "Doe"},
                
            };
            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }
            context.SaveChanges();

            
            // Populate Addresses
            var addresses = new PersonAddress[]
            {
                new PersonAddress{PersonId = clients.Single( c => c.LastName == "Kizer").PersonId, AddressLine1 = "123 Main St.", AddressLine2 = "Apt. B", City = "Towson", State = "MD", PostalCode = "21212"}
            };
            foreach (PersonAddress pa in addresses)
            {
                context.PersonAddresses.Add(pa);
            }
            context.SaveChanges();


        }
    }
}
