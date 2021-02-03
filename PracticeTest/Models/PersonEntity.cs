using System;
using System.ComponentModel.DataAnnotations;

namespace PracticeTest.Models
{
    public class PersonEntity
    {
        
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
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        
        [Required]
        public string Address { get; set; }

        public int SpouseId { get; set; }
        
        public string SpouseName { get; set; }

    }
   
}
