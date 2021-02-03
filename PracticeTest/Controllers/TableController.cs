using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
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
            var allPersons = _personContext.Person;
            return View(allPersons);
        }

        [HttpPost]
        public JsonResult GetPersonNames(int Id)
        {
            var recordsToSend = _personContext.Person.Except(_personContext.Person.Where(p => p.Id ==Id )).Where(p => p.SpouseId == 0).Select(p => new {p.FirstName, p.Id });
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult HandleForm(PersonEntity formData)
        {
            if (ModelState.IsValid)
            {
                _personContext.Person.Add(formData);
                _personContext.SaveChanges();
                return RedirectToAction("Table"); 
            }
            
            return View("Create",formData);
        }

        public IActionResult Delete(int Id)
        {
            PersonEntity recordToDelete = _personContext.Person.Where(record => record.Id == Id).First();
            _personContext.Person.Remove(recordToDelete);
            _personContext.SaveChanges();
            return RedirectToAction("Index"); 
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(PersonEntity person)
        {
            if (ModelState.IsValid)
            { 
                _personContext.Entry(person).State = EntityState.Modified;
                _personContext.SaveChanges();
                return RedirectToAction("Index"); 
            }
            return View(person);
        }

        public IActionResult Edit(int Id)
        {
            var record = _personContext.Person.Where(record => record.Id == Id).FirstOrDefault();
            return View(record); 
        }  
    }
}
