using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class PersonAddress
    {

        public long PersonAddressId { get; set; }

        public long PersonId { get; set; }
        
        [StringLength(100)]
        public string AddressLine1 { get; set; }

        [StringLength(100)]
        public string AddressLine2 { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string State { get; set; }

        [StringLength(9)]
        public string PostalCode { get; set; }

        public virtual Person Person { get; set; }
    }
}
