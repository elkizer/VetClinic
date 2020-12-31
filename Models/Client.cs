using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Client : Person
    {
        [Display(Name = "Appointments")]
        public virtual ICollection<Appointment> Appointments { get; set; }

        [Display(Name = "Pets")]
        public virtual ICollection<ClientAnimal> ClientAnimals { get; set; }
    }
}
