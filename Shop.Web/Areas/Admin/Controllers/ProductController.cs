using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public ShopDBContext Context { get; set; }
        public ProductController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var products = Context.Products.Include(p => p.Discount).Include(p => p.Category).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create() {
            return View();
        }

    }
}
