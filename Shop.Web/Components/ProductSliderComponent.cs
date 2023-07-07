using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data;

namespace Shop.Web.Components
{
    public class ProductSliderComponent : ViewComponent
    {
        public ShopDBContext Context { get; set; }
        public ProductSliderComponent(ShopDBContext context)
        {
            Context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var products = Context.Products.OrderBy(p => Guid.NewGuid()).Take(2);
            return View("Components/ProductSliderViewComponent.cshtml", products);
        }
    }
}
