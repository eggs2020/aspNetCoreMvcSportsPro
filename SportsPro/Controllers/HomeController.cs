using Microsoft.AspNetCore.Mvc;

namespace SportsPro.Controllers
{
    public class HomeController : Controller
    {
        //Action method for home index page
        public IActionResult Index()
        {
            return View();
        }

        //Action method for about page
        [Route("About")]
        public IActionResult About()
        {
            return View();
        }
    }
}