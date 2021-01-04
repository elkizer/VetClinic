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
            var employees = new Employee[]
            {
                new Employee{FirstName = "Jeanne", LastName = "Sparks", EmployeeTypeId = employeeTypes.Single(ea => ea.Code == "VET").EmployeeTypeId, HireDate = DateTime.Parse("2018-08-01")},
                new Employee{FirstName = "Jenny", LastName = "Palmer", EmployeeTypeId = employeeTypes.Single(ea => ea.Code == "TECH").EmployeeTypeId, HireDate = DateTime.Parse("2018-08-01")}
            };
            foreach (Employee e in employees)
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
                new PersonAddress{PersonId = clients.Single( c => c.LastName == "Bridges").PersonId, AddressLine1 = "22 West Blvd.", City = "Lutherville-Timonium", State = "MD", PostalCode = "21093"},
                new PersonAddress{PersonId = clients.Single( c => c.LastName == "Kizer").PersonId, AddressLine1 = "123 Main St.", AddressLine2 = "Apt. B", City = "Towson", State = "MD", PostalCode = "21212"},
                new PersonAddress{PersonId = clients.Single( c => c.LastName == "Doe").PersonId, AddressLine1 = "123 Main St.", AddressLine2 = "Apt. A", City = "Towson", State = "MD", PostalCode = "21212"},
                new PersonAddress{PersonId = employees.Single( e => e.LastName == "Sparks").PersonId, AddressLine1 = "2525 Eastridge Rd.", City = "Lutherville-Timonium", State = "MD", PostalCode = "21093"},
                new PersonAddress{PersonId = employees.Single( e => e.LastName == "Palmer").PersonId, AddressLine1 = "55 Westridge Rd.", City = "Lutherville-Timonium", State = "MD", PostalCode = "21093"}
            };
            foreach (PersonAddress pa in addresses)
            {
                context.PersonAddresses.Add(pa);
            }
            context.SaveChanges();

            // Populate Phone Numbers
            var phones = new PersonPhone[]
            {
                new PersonPhone{PersonId = clients.Single( c => c.LastName == "Bridges").PersonId, PhoneNumber = "410-123-1234"},
                new PersonPhone{PersonId = clients.Single( c => c.LastName == "Bridges").PersonId, PhoneNumber = "410-123-4321"},
                new PersonPhone{PersonId = clients.Single( c => c.LastName == "Kizer").PersonId, PhoneNumber = "410-456-1245"},
                new PersonPhone{PersonId = clients.Single( c => c.LastName == "Doe").PersonId, PhoneNumber = "410-785-7845"},
                new PersonPhone{PersonId = employees.Single( e => e.LastName == "Sparks").PersonId, PhoneNumber = "410-894-8541"},
                new PersonPhone{PersonId = employees.Single( e => e.LastName == "Palmer").PersonId, PhoneNumber = "410-785-7421"}
            };
            foreach (PersonPhone pp in phones)
            {
                context.PersonPhones.Add(pp);
            }
            context.SaveChanges();

            // Populate Emails
            var emails = new PersonEmail[]
            {
                new PersonEmail{PersonId = clients.Single( c => c.LastName == "Bridges").PersonId, EmailAddress = "chelsea.bridges@test.com"},
                new PersonEmail{PersonId = clients.Single( c => c.LastName == "Kizer").PersonId, EmailAddress = "rkizer@test.com"},
                new PersonEmail{PersonId = clients.Single( c => c.LastName == "Doe").PersonId, EmailAddress = "john.doe@test.com"},
                new PersonEmail{PersonId = employees.Single( e => e.LastName == "Sparks").PersonId, EmailAddress = "jsparks@test.com"},
                new PersonEmail{PersonId = employees.Single( e => e.LastName == "Palmer").PersonId, EmailAddress = "jpalmer@test.com"}
            };
            foreach (PersonEmail pe in emails)
            {
                context.PersonEmails.Add(pe);
            }
            context.SaveChanges();

            // Populate Pets
            var pets = new ClientAnimal[]
            {
                new ClientAnimal{ClientId = clients.Single( c => c.LastName == "Bridges").PersonId, Name = "Henry", BirthDate = DateTime.Parse("2018-08-01"), SpeciesId = species.Single(s => s.Code == "CANINE").SpeciesId},
                new ClientAnimal{ClientId = clients.Single( c => c.LastName == "Bridges").PersonId, Name = "Mudge", BirthDate = DateTime.Parse("2016-07-11"), SpeciesId = species.Single(s => s.Code == "CANINE").SpeciesId},
                new ClientAnimal{ClientId = clients.Single( c => c.LastName == "Bridges").PersonId, Name = "Kitty", BirthDate = DateTime.Parse("2019-06-2"), SpeciesId = species.Single(s => s.Code == "FELINE").SpeciesId},
                new ClientAnimal{ClientId = clients.Single( c => c.LastName == "Kizer").PersonId, Name = "Nash", BirthDate = DateTime.Parse("2018-08-01"), SpeciesId = species.Single(s => s.Code == "CANINE").SpeciesId},
                new ClientAnimal{ClientId = clients.Single( c => c.LastName == "Doe").PersonId, Name = "Fluffy II", BirthDate = DateTime.Parse("2018-08-01"), SpeciesId = species.Single(s => s.Code == "FELINE").SpeciesId}
            };
            foreach (ClientAnimal ca in pets)
            {
                context.ClientAnimals.Add(ca);
            }
            context.SaveChanges();

            // Populate Appointments
            var appointments = new Appointment[]
            {
                new Appointment{ClientAnimalId = pets.Single( p => p.Name == "Nash").ClientAnimalId, EmployeeId = employees.Single(e => e.LastName == "Palmer").PersonId, AppointmentDate = DateTime.Parse("2021-01-06 9:00"), Reason = "Ear cleaning"},
                new Appointment{ClientAnimalId = pets.Single( p => p.Name == "Nash").ClientAnimalId, EmployeeId = employees.Single(e => e.LastName == "Sparks").PersonId, AppointmentDate = DateTime.Parse("2021-02-05 9:30"), Reason = "Immunization"},
                new Appointment{ClientAnimalId = pets.Single( p => p.Name == "Kitty").ClientAnimalId, EmployeeId = employees.Single(e => e.LastName == "Sparks").PersonId, AppointmentDate = DateTime.Parse("2021-01-05 10:00"), Reason = "Immunization"},
                new Appointment{ClientAnimalId = pets.Single( p => p.Name == "Henry").ClientAnimalId, EmployeeId = employees.Single(e => e.LastName == "Palmer").PersonId, AppointmentDate = DateTime.Parse("2021-01-07 14:00"), Reason = "Nail trim"},
                new Appointment{ClientAnimalId = pets.Single( p => p.Name == "Mudge").ClientAnimalId, EmployeeId = employees.Single(e => e.LastName == "Palmer").PersonId, AppointmentDate = DateTime.Parse("2021-01-07 14:00"), Reason = "Nail trim"}
            };
            foreach (Appointment ap in appointments)
            {
                context.Appointments.Add(ap);
            }
            context.SaveChanges();


        }
    }
}