using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProductController : Controller
    {
        public ShopDBContext Context;
        public ProductController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CheckOutCart()
        {
            var order = Context.Orders.Include(o => o.OrderDetails)
            .ThenInclude(od => od.Product).FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value));
            return View(order);
        }
        [Route("/User/Product/Add/{ProductId}")]
        public IActionResult Add(int ProductId)
        {
            var order = Context.Orders.FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value));
            if (order == null)
            {
                Context.Orders.Add(new Models.Order()
                {
                    UserId = int.Parse(User.FindFirst("Id").Value),
                    isFinally = false,
                });
                Context.SaveChanges();
            }
            else
            {
                order = Context.Orders.FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value));
                var orderDetail = Context.OrderDetails.FirstOrDefault(od => od.ProductId == ProductId);
                if (orderDetail == null)
                {
                    Context.OrderDetails.Add(new Models.OrderDetail()
                    {
                        OrderId = order.Id,
                        ProductId = ProductId,
                        Quantity = 1
                    });
                    Context.SaveChanges();
                }
                else
                {
                    orderDetail.Quantity += 1;
                    Context.OrderDetails.Update(orderDetail);
                    Context.SaveChanges();
                }
                Context.SaveChanges();
            }
            return RedirectToAction("CheckOutCart", "Product");
        }

        [ValidateAntiForgeryToken]
        public IActionResult Buy()
        {
            if (Context.Orders.FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value)) == null)
            {
                return NotFound();
            }
            return NotFound();
        }
    }
}
