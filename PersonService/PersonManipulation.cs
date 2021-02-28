using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using PersonData;
using PersonData.Models;
using System;
using System.Collections.Generic;
using System.Linq;


namespace PersonService
{
    public class PersonManipulation : IPersonManipulation
    {
        private readonly PersonContext _context;
        public PersonManipulation(PersonContext context)
        {
            _context = context; 
        }
        public void AddPerson(IFormCollection form)
        {
            PersonEntity person = new PersonEntity
            {
                FirstName = form["firstName"],
                LastName = form["lastName"],
                BirthDate = Convert.ToDateTime(form["birthDate"]),
                PrimaryAddress = form["primaryAddress"],
                PhoneEntity = new List<PhoneNumberEntity>(),
                Addresses = new List<AddressEntity>()
            };

                foreach (string key in form["phoneNumber"])
                {
                    if (key != "")
                    {
                        person.PhoneEntity.Add(new PhoneNumberEntity { PhoneNumber = key });
                    }
                }

                if (form.ContainsKey("address"))
                {

                    foreach (string key in form["adress"])
                    {
                        if (key != "")
                        {
                            person.Addresses.Add(new AddressEntity { Address = key });
                        }
                    }
                }

                _context.Add(person);
                _context.SaveChanges();
        }

        public void DeletePerson(int id)
        {
            var personToDelete = GetPersonById(id);
            _context.Remove(personToDelete);
            _context.SaveChanges();
        }

        public IEnumerable<PersonEntity> GetAllPersons()
        {
            return _context.Person
                .Include(info => info.Addresses)
                .Include(info => info.PhoneEntity); 
        }

        public IEnumerable<PersonEntity> GetAvailablePersonNames(int id)
        {
            return GetAllPersons()
                 .Except((IEnumerable<PersonEntity>)GetPersonById(id))
                 .Where(person => person.SpouseId == 0);
        }

        public PersonEntity GetPersonById(int id)
        {
            return GetAllPersons()
                .FirstOrDefault(person => person.Id == id);
        }

        public void Marry(int personId, int spouseId)
        {
            var person1 = GetPersonById(personId);
            var person2 = GetPersonById(spouseId);

            person1.SpouseId = spouseId;
            person1.SpouseName = person2.FirstName;

            person2.SpouseId = personId;
            person2.SpouseName = person1.FirstName;


            _context.Entry(person1).State = EntityState.Modified;
            _context.Entry(person2).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
