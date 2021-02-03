using Microsoft.AspNetCore.Mvc;


namespace PracticeTest.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }  
    }
}
