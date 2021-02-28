using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeTest.ViewModel;
using PersonData.Models;
using PersonData;

namespace PracticeTest.Controllers
{
    public class TableController : Controller
    {

        private IPersonManipulation _personService; 

        public TableController(IPersonManipulation personService)
        {
            _personService = personService;
        }

        public IActionResult Index()
        {
            PersonViewModel viewModel = new PersonViewModel() {
                Persons = _personService.GetAllPersons()
            };

            return View(viewModel);
        }

        [HttpPost]
        public JsonResult GetPersonNames(int id)
        {
            var recordsToSend = _personService.GetAvailablePersonNames(id); 
            return Json(recordsToSend);
        }


        [HttpPost]
        public void MarryTwoPerson(int personId, int spouseId)
        {
            _personService.Marry(personId, spouseId); 
        }

        [HttpPost]
        public IActionResult PopUpForm(IFormCollection form)
        {
            TryUpdateModelAsync(form);

           if(ModelState.IsValid)
            {
                _personService.AddPerson(form);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _personService.DeletePerson(id);
            return RedirectToAction("Index");
        }

    }
}
