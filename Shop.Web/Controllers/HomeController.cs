using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Models;

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
            var products = Context.Products.Include(p => p.Category).ToList().Take(12);
            return View(products);
        }

        public IActionResult Category(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var ProductsOfCategory = Context.Categories.Include(c => c.Products).FirstOrDefault(p => p.Id == Id);
            return View(ProductsOfCategory);
        }

        public IActionResult Product(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var Product = Context.Products.Include(c => c.Category)
                .FirstOrDefault(p => p.Id == Id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

    }
}

