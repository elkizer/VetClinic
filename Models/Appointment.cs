using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class Appointment
    {
        public long ID { get; set; }
        public long ClientID { get; set; }
        public long AnimalID { get; set; }
        public long EmployeeID { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime AppointmentDate { get; set; }
        public string Reason { get; set; }

        public Client Client { get; set; }
        public Animal Animal { get; set; }
        public Employee Employee { get; set; }
    }
}
