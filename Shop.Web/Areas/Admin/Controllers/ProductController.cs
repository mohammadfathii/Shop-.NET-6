using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Data.Services.Interfaces;
using Shop.Web.Models;
using Shop.Web.Models.ViewModel;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        public ShopDBContext Context { get; set; }
        public IServerSideService ServerSideService { get; set; }
        public List<Category> Categories { get; set; }
        public ProductController(ShopDBContext context,IServerSideService serverSideService)
        {
            Context = context;
            ServerSideService = serverSideService;
            Categories = Context.Categories.ToList();
        }
        public IActionResult Index()
        {
            var products = Context.Products.Include(p => p.Category).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create() {
            return View(new CreateUpdateProductViewModel()
            {
                Categories = Categories
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateProductViewModel product)
        {
            product.Categories = Categories;
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("Name", "Something is wrong");
                return View(product);
            }
            if (product.Price < 10000)
            {
                ModelState.AddModelError("Price","قیمت محصول نمیتواند کمتر از هزار تومان باشد");
                return View(product);
            }
            var thumbnail = "user";
            if (product.Thumbnail.Length > 0 )
            {
                thumbnail = await ServerSideService.UploadFile(product.Thumbnail,@"Images\Products\");
            }

            var Product = new Product()
            {
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryId = int.Parse(product.CategoryId.ToString()),
                Thumbnail = thumbnail,
                QuantityInStock = product.QuantityInStock,
            };
            if (product.DiscountPercent > 0)
            {
                Product.DiscountPercent = product.DiscountPercent;
                Product.DiscountCount = product.DiscountCount;
            }

            Context.Products.Add(Product);
            Context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
