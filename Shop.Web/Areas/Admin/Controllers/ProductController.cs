﻿using Microsoft.AspNetCore.Mvc;
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
        public ProductController(ShopDBContext context,IServerSideService serverSideService)
        {
            Context = context;
            ServerSideService = serverSideService;
        }
        public IActionResult Index()
        {
            var products = Context.Products.Include(p => p.Discount).Include(p => p.Category).ToList();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create() {
            var categories = Context.Categories.ToList();
            return View(new CreateUpdateProductViewModel()
            {
                Categories = categories
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateUpdateProductViewModel product)
        {
            if (!ModelState.IsValid)
            {
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
                CategoryId = product.CategoryId,
                Thumbnail = thumbnail,
            };
            Context.Products.Add(Product);

            
            if (product.DiscountPercent > 0 && product.DiscountPercent < 100)
            {
                var discount = new Discount()
                {
                    Count = product.DiscountCount,
                    DiscountPercent = product.DiscountPercent,
                    ProductId = Product.Id
                };
                Context.Discounts.Add(discount);
                Context.SaveChanges();
                Product.DiscountId = discount.Id;
                return RedirectToAction("Product");
            }

            Context.SaveChanges();
            return RedirectToAction("Product");
        }

    }
}
