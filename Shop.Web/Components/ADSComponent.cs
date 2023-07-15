using Microsoft.AspNetCore.Mvc;
using Shop.Web.Data;

namespace Shop.Web.Components
{
    public class ADSComponent : ViewComponent
    {
        public ShopDBContext Context { get; set; }
        public ADSComponent(ShopDBContext context)
        {
            Context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var currentTime = DateTime.UtcNow;
            var randomAds = Context.ADs.OrderBy(x => Guid.NewGuid()).Where(ad => ad.ExpireTime >= currentTime).Take(5).ToList();
            return View("Components/ADSViewComponent.cshtml",randomAds);
        }

    }
}
