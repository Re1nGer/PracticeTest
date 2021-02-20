using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeTest.ViewModel; 
using PracticeTest.Models;

namespace PracticeTest.Controllers
{
    public class TableController : Controller
    {

        private readonly PersonContext _personContext;

        public TableController(PersonContext personContext)
        {
            _personContext = personContext;

        }

        public IActionResult Index()
        {
            PersonViewModel viewModel = new PersonViewModel() { Persons = _personContext.Person };

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult GetPersonNames(int Id)
        {
            var recordsToSend = _personContext.Person.Except(_personContext.Person.Where(p => p.Id == Id)).Where(p => p.SpouseId == 0).Select(p => new { p.FirstName, p.Id });
            return Json(recordsToSend);
        }

        [HttpPost]
        public void MarryTwoPerson(int PersonId, int SpouseId)
        {
            var firstRecord = _personContext.Person.Find(PersonId);
            var secondRecord = _personContext.Person.Find(SpouseId);

            firstRecord.SpouseId = SpouseId;
            firstRecord.SpouseName = secondRecord.FirstName;

            secondRecord.SpouseId = PersonId;
            secondRecord.SpouseName = firstRecord.FirstName;

            _personContext.Entry(firstRecord).State = EntityState.Modified;
            _personContext.Entry(secondRecord).State = EntityState.Modified;
            _personContext.SaveChanges();
        }

        [HttpPost]
        public IActionResult PopUpForm(IFormCollection form)
        {
            PersonViewModel viewModel = new PersonViewModel() { Persons = _personContext.Person };

            PersonEntity person = new PersonEntity
            {
                FirstName = form["firstName"],
                LastName = form["lastName"],
                BirthDate = Convert.ToDateTime(form["birthDate"]),
                PrimaryAddress = form["primaryAddress"],
                PhoneEntity = new List<PhoneNumberEntity>(),
                Addresses = new List<AddressEntity>()
            };

            TryUpdateModelAsync(person);


            if (ModelState.IsValid)
            {

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

                _personContext.Add(person);
                _personContext.SaveChanges();
            }

            return View("Index", viewModel);
        }

        public IActionResult Delete(int Id)
        {
            PersonEntity recordToDelete = _personContext.Person.Where(record => record.Id == Id).Include(record => record.Addresses).Include(record => record.PhoneEntity).First();
            _personContext.Remove(recordToDelete);
            _personContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
