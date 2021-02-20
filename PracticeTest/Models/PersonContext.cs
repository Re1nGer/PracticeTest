using Microsoft.EntityFrameworkCore;


namespace PracticeTest.Models
{
    public class PersonContext : DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {

        }
        public DbSet<PersonEntity> Person { get; set; }
        public DbSet<PhoneNumberEntity> PhoneNumbers { get; set; }
        public DbSet<AddressEntity> Addresses { get; set; } 
    }
}

