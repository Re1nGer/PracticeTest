using Microsoft.AspNetCore.Http;
using PersonData.Models;
using System.Collections.Generic;


namespace PersonData
{
    public interface IPersonManipulation
    {
        void AddPerson(IFormCollection form);
        void DeletePerson(int id);
        void Marry(int personId, int spouseId);
        IEnumerable<PersonEntity> GetAvailablePersonNames(int id);
        IEnumerable<PersonEntity> GetAllPersons();
        PersonEntity GetPersonById(int id); 
    }
}
