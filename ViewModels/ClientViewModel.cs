using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VetClinic.Models;

namespace VetClinic.ViewModels
{
    public class ClientViewModel
    {
        public IEnumerable<Client> Clients { get; set; }
        public IEnumerable<Animal> Animals { get; set; }
        public IEnumerable<Appointment> Appointments { get; set; }
    }
}
