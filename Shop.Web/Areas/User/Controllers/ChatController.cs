using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
