using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        public ShopDBContext Context { get; set; }
        public CategoryController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var categories = Context.Categories.Include(c => c.Products).ToList();
            return View(categories);
        }
    }
}
