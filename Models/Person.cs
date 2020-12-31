using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public abstract class Person
    {
        public long PersonId { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "First name must be longer than 50 characters.")]
        [Column("FirstName")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Last name must be longer than 50 characters.")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }

        public virtual ICollection<PersonAddress> PersonAddresses { get; set; }
        public virtual ICollection<PersonPhone> PersonPhones { get; set; }
        public virtual ICollection<PersonEmail> PersonEmails { get; set; }
    }
}
