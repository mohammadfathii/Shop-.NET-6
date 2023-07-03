    using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shop.Web.Data;
using Shop.Web.Models.ViewModel;

namespace Shop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        public ShopDBContext Context { get; set; }
        public UserController(ShopDBContext context)
        {
            Context = context;
        }
        public IActionResult Index()
        {
            var users = Context.Users
                .Include(u => u.Orders)
                .Include(u => u.Comments)
                .Include(u => u.Chats)
                .Include(u => u.SubComments)
                .ToList();
            return View(users);
        }

        public IActionResult Details(int id)
        {
            var user = Context.Users.FirstOrDefault(u => u.Id == id);
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit(int id) {
            var user = Context.Users.FirstOrDefault(u => u.Id == id);
            var User = new CreateUpdateUserViewModel()
            {
                Id = id,
                FullName = user.FullName,
                UserName = user.UserName,
                Password = user.Password,
                Email = user.Email,
                VerifyToken = user.VerifyToken,
                IsAdmin = user.IsAdmin
            };
            return View(User);
        }


    }
}
