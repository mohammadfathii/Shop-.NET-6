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
            var products = Context.Products.Include(p => p.Category).Include(p => p.Discount).ToList();
            return View(products);
        }

        public IActionResult Category(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var ProductsOfCategory = Context.Categories.Include(c => c.Products).FirstOrDefault(p => p.Id == Id).Products.ToList();
            return View();
        }

        public IActionResult Product(int Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            var Product = Context.Products.Include(c => c.Category).Include(p=>p.Discount)
                .FirstOrDefault(p => p.Id == Id);
            if (Product == null)
            {
                return NotFound();
            }
            return View(Product);
        }

        #region Tests
        public IActionResult TestArticleViewModel()
        {
            return View("Product",new Product()
            {
                Id = 2,
                Name = "Test",
                Thumbnail = "Test",
                Price = 20000,
                Description = "Test",
                QuantityInStock = 5,
                Discount = new Discount() {
                    Id = 3,
                    Count = 12,
                    DiscountPercent = 20,
                    ExpireTime = DateTime.Now,
                }
                ,DiscountId = 3,
                Category = new Category()
                {
                    Id = 1,
                    Name = "Test Category" ,
                    Description = "Test"
                },
                CategoryId = 1
            });
        }

        public IActionResult TestCategoryViewModel()
        {
            return View("Category", new Category()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Products = new List<Product>()
            });
        }


        #endregion
        

    }
}

