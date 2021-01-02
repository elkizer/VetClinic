using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Appointment
    {
        public long AppointmentId { get; set; }
        public long ClientAnimalId { get; set; }
        public long EmployeeId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:f}", ApplyFormatInEditMode = true)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }

        [Display(Name = "Pet")]
        public virtual ClientAnimal ClientAnimal { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
