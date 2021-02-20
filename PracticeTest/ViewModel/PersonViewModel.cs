using System;
using System.Collections.Generic;
using PracticeTest.Models; 

namespace PracticeTest.ViewModel
{
    public class PersonViewModel
    {
        public IEnumerable<PersonEntity> Persons { get; set; }
        public PersonEntity Person { get; set; }
    }
}
