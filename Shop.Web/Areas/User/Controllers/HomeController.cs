using Microsoft.AspNetCore.Mvc;
using Shop.Web.Areas.User.Models.ViewModel;
using Shop.Web.Data;
using Shop.Web.Data.Services;
using Shop.Web.Data.Services.Interfaces;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public ShopDBContext Context { get; set; }
        public IServerSideService ServerSideService { get; set; }
        public HomeController(ShopDBContext context,IServerSideService serverSideService)
        {
            Context = context;
            ServerSideService = serverSideService;
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
                Address1 = user.Address,
                Address2 = user.Address2,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
            });
        }
        [HttpPost]
        public async Task<IActionResult> EditProfileAsync(CreateUpdateUserViewModel user)
        {
            var U = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));

            if (user.Id != U.Id)
            {
                return NotFound();
            }

            else if (!ModelState.IsValid)
            {
                return View(user);
            }

            if (user.Image != null && user.Image.Length > 0)
            {
                ServerSideService.DeleteFile(@"Images\Avatars\" + U.Avatar);
                var image = await ServerSideService.UploadFile(user.Image, @"Images\Avatars\");
                U.Avatar = image;
            }else
            {
                U.Avatar = "user";
            }

            U.FullName = user.FullName;
            U.City = user.City;
            U.Address = user.Address1;
            U.Address2 = user.Address2;
            U.PhoneNumber = user.PhoneNumber;

            Context.Update(U);
            Context.SaveChanges();

            return View();
        }
        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordUserViewModel User)
        {
            return View();
        }

    }
}
