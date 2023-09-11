using Microsoft.AspNetCore.Mvc;
using Shop.Web.Areas.User.Models.ViewModel;
using Shop.Web.Data;
using Shop.Web.Models;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public ShopDBContext Context { get; set; }
        public HomeController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));
            return View(user);
        }

        public ActionResult Profile()
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));

            return View(user);
        }

        [HttpGet]
        public ActionResult EditProfile()
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));

            return View(new CreateUpdateUserViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                City = user.City,
                Address = user.Address,
                Address2 = user.Address2,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
            });
        }
        [HttpPost]
        public IActionResult EditProfile(CreateUpdateUserViewModel user)
        {
            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordUserViewModel User)
        {
            return View();
        }

    }
}
