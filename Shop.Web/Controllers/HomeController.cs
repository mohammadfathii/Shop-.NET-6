using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            Console.WriteLine("IP : " + GetIP());
            return View();
        }
    }
}
