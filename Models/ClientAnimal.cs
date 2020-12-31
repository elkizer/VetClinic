using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class ClientAnimal
    {
        public long ClientId { get; set; }
        public Client Client { get; set; }

        public long AnimalId { get; set; }
        public Animal Animal { get; set; }
    }
}
