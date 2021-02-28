using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PersonData.Models
{
    public class AddressEntity
    {
        [Key]
        public int AddressId { get; set; }

        public string Address { get; set; }

        public int PersonId { get; set; }


    }
}
