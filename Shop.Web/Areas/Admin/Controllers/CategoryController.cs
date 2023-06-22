using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Models;
using Shop.Web.Models.ViewModel;

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
        [HttpGet]
        public IActionResult Create() {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreateUpdateCategoryViewModel category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var Category = new Category()
            {
                Name = category.Name,
                Description = category.Description,
            };
            Context.Categories.Add(Category);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
