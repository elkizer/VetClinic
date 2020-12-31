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

            // Populuate animals
            var animals = new Animal[]
            {
                new Animal{SpeciesId = species.Single(s => s.Code == "CANINE").SpeciesId, Name = "Henry", BirthDate = DateTime.Parse("2018-08-01") },
                new Animal{SpeciesId = species.Single(s => s.Code == "CANINE").SpeciesId, Name = "Mudge", BirthDate = DateTime.Parse("2020-09-01") },
                new Animal{SpeciesId = species.Single(s => s.Code == "CANINE").SpeciesId, Name = "Nash", BirthDate = DateTime.Parse("2017-05-10") },
                new Animal{SpeciesId = species.Single(s => s.Code == "FELINE").SpeciesId, Name = "Kitty", BirthDate = DateTime.Parse("2010-04-01") },
            };
            foreach (Animal a in animals)
            {
                context.Animals.Add(a);
            }
            context.SaveChanges();

            // Populate ClientAnimals
            var clientAnimals = new ClientAnimal[]
            {
                new ClientAnimal{ClientId = clients.Single(c => c.LastName == "Bridges").PersonId, AnimalId = animals.Single(a => a.Name == "Henry").AnimalId },
                new ClientAnimal{ClientId = clients.Single(c => c.LastName == "Bridges").PersonId, AnimalId = animals.Single(a => a.Name == "Mudge").AnimalId },
                new ClientAnimal{ClientId = clients.Single(c => c.LastName == "Kizer").PersonId, AnimalId = animals.Single(a => a.Name == "Nash").AnimalId },
                new ClientAnimal{ClientId = clients.Single(c => c.LastName == "Doe").PersonId, AnimalId = animals.Single(a => a.Name == "Kitty").AnimalId },
            };
            foreach (ClientAnimal ca in clientAnimals)
            {
                context.ClientAnimals.Add(ca);
            }
            context.SaveChanges();

        }
    }
}
