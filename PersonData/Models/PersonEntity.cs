using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace PersonData.Models
{
    public class PersonEntity
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Birth Date")]

        public DateTime BirthDate { get; set; }
        
        [Required]
        [Display(Name= "Phone Number")]
        [ForeignKey("PersonId")]
        public virtual ICollection<PhoneNumberEntity> PhoneEntity { get; set; }

        [ForeignKey("PersonId")]
        public virtual ICollection<AddressEntity> Addresses { get; set; }

        [Required]
        public string PrimaryAddress { get; set; }

        public int SpouseId { get; set; }
        
        public string SpouseName { get; set; }

    }
   
}
