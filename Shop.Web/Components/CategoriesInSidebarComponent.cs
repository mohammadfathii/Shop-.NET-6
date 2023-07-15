using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;

namespace Shop.Web.Components
{
    public class CategoriesInSidebarComponent : ViewComponent
    {
        public ShopDBContext Context { get; set; }
        public CategoriesInSidebarComponent(ShopDBContext context)
        {
            Context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = Context.Categories.Include(c => c.Products).ToList();
            return View("Components/CategoriesInSidebarViewComponent.cshtml", categories);
        }
    }
}
