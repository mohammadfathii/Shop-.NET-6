using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;

namespace Shop.Web.Controllers
{
    public class HomeController : Controller
    {
        public ShopDBContext Context { get; set; }
        public HomeController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var products = Context.Products.Include(p => p.Categories).Include(p => p.Discount).ToList();
            return View(products);
        }
    }
}

