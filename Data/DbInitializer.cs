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


            if (context.Clients.Any())
            {
                return;
            }

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


            if (context.Animals.Any())
            {
                return;
            }

            var animals = new Animal[]
            {
                new Animal{SpeciesID = 1, Name = "Henry", BirthDate = DateTime.Parse("2018-08-01") },
                new Animal{SpeciesID = 1, Name = "Mudge", BirthDate = DateTime.Parse("2020-09-01") },
                new Animal{SpeciesID = 1, Name = "Nash", BirthDate = DateTime.Parse("2017-05-10") },
                new Animal{SpeciesID = 2, Name = "Kity", BirthDate = DateTime.Parse("2010-04-01") },
            };
            foreach (Animal a in animals)
            {
                context.Animals.Add(a);
            }
            context.SaveChanges();

        }
    }
}
