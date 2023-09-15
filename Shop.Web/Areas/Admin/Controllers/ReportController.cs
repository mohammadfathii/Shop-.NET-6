using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Models;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ReportController : Controller
    {
        public ShopDBContext ShopDBContext { get; set; }
        private INotyfService _toastNotification { get; set; }

        public ReportController(ShopDBContext shopDBContext,INotyfService notyfService)
        {
            ShopDBContext = shopDBContext;
            _toastNotification = notyfService;
        }
        public IActionResult Index()
        {
            var reports = ShopDBContext.Reports.Where(r => r.IsFinally == false).Include(r => r.User).Include(r => r.ReportMessages);
            return View(reports);
        }

        public IActionResult Show(int Id)
        {
            var report = ShopDBContext.Reports.Include(r => r.ReportMessages).ThenInclude(rm => rm.User).FirstOrDefault(r => r.Id == Id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        public IActionResult Close(int Id)
        {
            var report = ShopDBContext.Reports.FirstOrDefault(r => r.Id == Id);
            if (report == null)
            {
                return NotFound();
            }
            report.IsFinally = true;
            ShopDBContext.Reports.Update(report);
            ShopDBContext.SaveChanges();
            _toastNotification.Warning("ریپورت به درستی بسته شد !");
            return RedirectToAction("Index");
        }

        public IActionResult CloseList(int Id)
        {
            var reports = ShopDBContext.Reports.Where(r => r.UserId == int.Parse(User.FindFirst("Id").Value) && r.IsFinally == true).Include(c => c.ReportMessages).Include(r => r.User);
            return View(reports);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Message(int Id, string Body)
        {
            var report = ShopDBContext.Reports.FirstOrDefault(r => r.Id == Id && r.UserId == int.Parse(User.FindFirst("Id").Value));

            if (report == null)
            {
                return RedirectToAction("Index");
            }

            if (report.IsFinally == false)
            {
                ShopDBContext.ReportMessages.Add(new ReportMessage()
                {
                    UserId = int.Parse(User.FindFirst("Id").Value),
                    ReportId = Id,
                    Body = Body,
                });
                ShopDBContext.SaveChanges();
                _toastNotification.Success("پیام فرستاده شد !");
                return RedirectToAction("Show", new
                {
                    Id = Id
                });
            }

            return RedirectToAction("Index");
        }

    }
}
