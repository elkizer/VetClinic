using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Client : Person
    {
        public virtual ICollection<Appointment> Appointments { get; set; }

        public virtual ICollection<ClientAnimal> ClientAnimals { get; set; }
    }
}
