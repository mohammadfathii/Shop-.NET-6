using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;

namespace Shop.Web.Components
{

    public class TopCategoriesComponent : ViewComponent
    {
        public ShopDBContext Context;
        public TopCategoriesComponent(ShopDBContext context)
        {
            Context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = Context.Categories.Include(c => c.Products).OrderByDescending(c => c.Products.Count()).Take(4).ToList();
            return View("Components/TopCategoriesViewComponent.cshtml", categories);
        }
    }
}