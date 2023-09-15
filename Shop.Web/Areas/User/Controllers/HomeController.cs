using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shop.Web.Areas.User.Models.ViewModel;
using Shop.Web.Data;
using Shop.Web.Data.Services;
using Shop.Web.Data.Services.Interfaces;
using Shop.Web.Models;

namespace Shop.Web.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        public ShopDBContext Context { get; set; }
        public IServerSideService ServerSideService { get; set; }
        public CreateUpdateUserViewModel TheUser { get; set; }
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
        public async Task<IActionResult> EditProfile()
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));
            TheUser = new CreateUpdateUserViewModel()
            {
                Id = user.Id,
                FullName = user.FullName,
                City = user.City,
                Address1 = user.Address,
                Address2 = user.Address2,
                PhoneNumber = user.PhoneNumber,
                Avatar = user.Avatar,
            };

            return View(TheUser);
        }
        [HttpPost]
        public async Task<IActionResult> EditProfile(CreateUpdateUserViewModel user)
        {
            var U = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));

            TheUser = new CreateUpdateUserViewModel()
            {
                Id = U.Id,
                FullName = U.FullName,
                City = U.City,
                Address1 = U.Address,
                Address2 = U.Address2,
                PhoneNumber = U.PhoneNumber,
                Avatar = U.Avatar,
            };

            if (user.Id != U.Id)
            {
                return NotFound();
            }

            U.FullName = user.FullName;
            U.City = user.City;
            U.Address = user.Address1;
            U.Address2 = user.Address2;
            U.PhoneNumber = user.PhoneNumber;

            if (user.Image != null && user.Image.Length > 0)
            {
                ServerSideService.DeleteFile(@"Images\Avatars\" + U.Avatar);
                var image = await ServerSideService.UploadFile(user.Image, @"Images\Avatars\");
                U.Avatar = image;
            }else
            {
                U.Avatar = U.Avatar;
            }
            Console.WriteLine(U);
            Context.Update(U);
            Context.SaveChanges();

            return View(TheUser);
        }

        public IActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword(ChangePasswordUserViewModel user)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            var U = Context.Users.FirstOrDefault(u => u.Id == int.Parse(User.FindFirst("Id").Value));

            if (U == null)
            {
                return NotFound();
            }

            U.Password = user.NewPassword;
            Context.Users.Update(U);
            Context.SaveChanges();
            return RedirectToAction("Profile");
        }

    }
}
