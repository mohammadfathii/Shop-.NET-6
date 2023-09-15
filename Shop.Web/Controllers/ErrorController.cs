using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.Controllers
{
    public class ErrorController : Controller
    {
        [HttpGet("/Error/{statuscode}")]
        public IActionResult Error(int statuscode)
        {
            if (statuscode == 404)
            {
                return View("404");
            }else if (statuscode == 500)
            {
                return View("500");
            }
            return View();
        }
    }
}
