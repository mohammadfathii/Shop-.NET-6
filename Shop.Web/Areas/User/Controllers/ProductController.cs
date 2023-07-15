using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProductController : Controller
    {
        public ShopDBContext Context;
        public ProductController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckOutCart()
        {
            var order = Context.Orders.FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value));
            return View(order);
        }
    }
}
