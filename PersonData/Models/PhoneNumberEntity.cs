using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PersonData.Models
{
    public class PhoneNumberEntity
    {
        [Key]
        public int Number_Id { get; set; }

        public int PersonId { get; set; }

        public string PhoneNumber { get; set; }


    }
}
