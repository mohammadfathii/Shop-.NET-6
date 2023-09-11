using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Models;
using ZarinpalSandbox;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    [Authorize]
    public class ProductController : Controller
    {
        public ShopDBContext Context;
        public object _locker = new object();
        public object _MinusLocker = new object();
        public object _BuyLocker = new object();
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
            lock (_locker)
            {
                var order = Context.Orders.FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value));
                if (order == null)
                {
                    var neworder = new Shop.Web.Models.Order()
                    {
                        UserId = int.Parse(User.FindFirst("Id").Value),
                        isFinally = false,
                    };
                    Context.Orders.Add(neworder);
                    Context.SaveChanges();


                    Context.OrderDetails.Add(new Shop.Web.Models.OrderDetail()
                    {
                         OrderId = neworder.Id,
                         ProductId = ProductId,
                         Quantity = 1
                    });
                    Context.SaveChanges();
                }
                else
                {
                    order = Context.Orders.Include(od => od.OrderDetails).ThenInclude(od => od.Product).FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value));
                    var orderDetail = Context.OrderDetails.Include(od => od.Product).FirstOrDefault(od => od.ProductId == ProductId && od.Orderr.isFinally == false); ;
                    if (orderDetail == null)
                    {
                        if (Context.Products.Find(ProductId).QuantityInStock >= 1)
                        {
                            Context.OrderDetails.Add(new Shop.Web.Models.OrderDetail()
                            {
                                OrderId = order.Id,
                                ProductId = ProductId,
                                Quantity = 1
                            });
                            Context.SaveChanges();
                        }
                    }
                    else
                    {
                        if (orderDetail.Product.QuantityInStock >= orderDetail.Quantity)
                        {
                            orderDetail.Quantity += 1;
                            Context.OrderDetails.Update(orderDetail);
                        }
                        Context.SaveChanges();
                    }
                    Context.SaveChanges();
                }
                return RedirectToAction("CheckOutCart", "Product");
            }
        }
        [Route("/User/Product/Minus/{OrderDetailId}")]
        public IActionResult Minus(int OrderDetailId)
        {
            lock (_MinusLocker)
            {
                var order = Context.Orders.Include(o => o.OrderDetails).FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(User.FindFirst("Id").Value));
                if (order != null)
                {
                    var od = order.OrderDetails.FirstOrDefault(od => od.Id == OrderDetailId);
                    if (od != null)
                    {
                        if (od.Quantity == 1)
                        {
                            Context.OrderDetails.Remove(od);
                            Context.SaveChanges();
                            return RedirectToAction("CheckOutCart", "Product");
                        }
                        od.Quantity -= 1;
                        Context.OrderDetails.Update(od);
                        Context.SaveChanges();
                        return RedirectToAction("CheckOutCart", "Product");
                    }
                }
                return RedirectToAction("CheckOutCart", "Product");
            }
        }

        public IActionResult Buy()
        {
            // if(Context.Users.FindAsync()){

            // }
            var userId = User.FindFirst("Id").Value;
            var order = Context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).FirstOrDefault(o => o.isFinally == false && o.UserId == int.Parse(userId));
            if (order == null)
            {
                return NotFound();
            }
            var totalPrices = order.OrderDetails.Sum(od => od.Product.DiscountPercent > 0 ? od.Quantity * (od.Product.Price - (od.Product.Price * od.Product.DiscountPercent / 100)) : od.Quantity * od.Product.Price);
            var payment = new Payment((int)totalPrices);
            var res = payment.PaymentRequest($"پرداخت فاکتور شماره {order.Id}", "https://localhost:7056/User/Product/OnlinePayment/" + order.Id, "m_fathi65@yahoo.com", "0900000000");
            if (res.Result.Status == 100)
            {
                System.Console.WriteLine("Payment worked !");
                return Redirect("https://sandbox.zarinpal.com/pg/StartPay/" + res.Result.Authority);
            }
            else
            {
                System.Console.WriteLine("Payment dont worked !");
                return BadRequest();
            }
        }
        [HttpGet("/User/Product/OnlinePayment/{id}")]
        public IActionResult OnlinePayment(int id)
        {
            lock (_BuyLocker) {
                if (HttpContext.Request.Query["Status"] != "" &&
                    HttpContext.Request.Query["Status"].ToString().ToLower() == "ok" &&
                    HttpContext.Request.Query["Authority"] != "")
                {
                    string authority = HttpContext.Request.Query["Authority"].ToString();
                    var order = Context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                        .FirstOrDefault(o => o.Id == id);
                    var totalPrices = order.OrderDetails.Sum(od => od.Product.DiscountPercent > 0 ? od.Quantity * (od.Product.Price - (od.Product.Price * od.Product.DiscountPercent / 100)) : od.Quantity * od.Product.Price);

                    var payment = new Payment((int)totalPrices);
                    var res = payment.Verification(authority).Result;
                    if (res.Status == 100)
                    {
                        order.isFinally = true;
                        Context.Orders.Update(order);
                        Context.SaveChanges();
                        ViewBag.code = res.RefId;
                        return View();
                    }
                }
                return NotFound();
            }
        }
    
        public IActionResult Factors ()
        {
            var userId = User.FindFirst("Id").Value;
            var factors = Context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product).Where(o => o.isFinally == true && o.UserId == int.Parse(userId)).ToList();
            return View(factors);
        }
    }
}
