using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Models;
using Shop.Web.Models.ViewModel;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    public class ReportController : Controller
    {
        public ShopDBContext ShopDBContext { get; set; }
        public ReportController(ShopDBContext context)
        {
            ShopDBContext = context;
        }
        public IActionResult Index()
        {
            var reports = ShopDBContext.Reports.Where(r => r.UserId == int.Parse(User.FindFirst("Id").Value) && r.IsFinally == false).Include(c => c.ReportMessages).Include(r => r.User);

            return View(reports);
        }

        public IActionResult Report(int Id) {
            var product = ShopDBContext.Products.FirstOrDefault(p => p.Id == Id);
            ShopDBContext.Reports.Add(new Report()
            {
                Title = product.Name,
                Body = ("Product ID : " + product.Id).ToString(),
                IsFinally = false,
                UserId = int.Parse(User.FindFirst("Id").Value)
            });
            ShopDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Show(int Id) {
            var report = ShopDBContext.Reports.Include(r => r.ReportMessages).ThenInclude(rm => rm.User).FirstOrDefault(r => r.Id == Id && r.UserId == int.Parse(User.FindFirst("Id").Value));
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        public IActionResult Close(int Id) {
            var report = ShopDBContext.Reports.FirstOrDefault(r => r.Id == Id);
            if (report == null)
            {
                return NotFound();
            }
            report.IsFinally = true;
            ShopDBContext.Reports.Update(report);
            ShopDBContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult CloseList(int Id) {
            var reports = ShopDBContext.Reports.Where(r => r.UserId == int.Parse(User.FindFirst("Id").Value) && r.IsFinally == true).Include(c => c.ReportMessages).Include(r => r.User);
            return View(reports);
        }

        public IActionResult Message(ReportMessageViewModel RMVM)
        {
            var report = ShopDBContext.Reports.FirstOrDefault(r => r.Id == RMVM.ReportId);

            if (report == null)
            {
                return BadRequest();
            }else if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            ShopDBContext.ReportMessages.Add(new ReportMessage()
            {
                UserId = int.Parse(User.FindFirst("Id").Value),
                ReportId = RMVM.ReportId,
                Body = RMVM.Body,
            });
            ShopDBContext.SaveChanges();

            return RedirectToAction("Show",new
            {
                Id = RMVM.ReportId
            });
        }
    }
}
