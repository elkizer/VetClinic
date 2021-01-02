using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VetClinic.Models
{
    public class ClientAnimal
    {
        public long ClientAnimalId { get; set; }

        public long ClientId { get; set; }

        public int SpeciesId { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }

        public string Notes { get; set; }

        public virtual Species Species { get; set; }

        public virtual Client Client { get; set; }

    }
}
