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
        public long ClientId { get; set; }
        public long AnimalId { get; set; }
        public long EmployeeId { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Appointment Date")]
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }

        public virtual Client Client { get; set; }
        public virtual Animal Animal { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
