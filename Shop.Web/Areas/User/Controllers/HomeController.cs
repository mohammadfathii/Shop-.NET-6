using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public ShopDBContext Context { get; set; }
        public HomeController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));
            return View(user);
        }
    }
}
