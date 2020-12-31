using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class PersonEmail
    {
        public long PersonEmailId { get; set; }

        public long PersonId { get; set; }

        [Display(Name = "Email address")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public virtual Person Person { get; set; }
    }
}
