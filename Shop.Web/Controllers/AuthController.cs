using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models.ViewModel;
using Shop.Web.Models;
using Shop.Web.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Shop.Web.Data.Services.Interfaces;

namespace Shop.Web.Controllers
{
    public class AuthController : Controller
    {
        public ShopDBContext Context { get; set; }
        public IServerSideService ServerSideService { get; set; }
        public AuthController(ShopDBContext context,IServerSideService serverSideService)
        {
            Context = context;
            ServerSideService = serverSideService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel Register)
        {
            Register.Email = Register.Email.ToLower();
            Register.UserName = Register.UserName.ToLower();
            if (!ModelState.IsValid) {
                return View(Register);
            }
            if (Context.Users.Any(u => u.UserName == Register.UserName || u.Email == Register.Email))
            {
                ModelState.AddModelError("Email","Email is duplicated !");
                ModelState.AddModelError("UserName","UserName is duplicated !");
                return View(Register);
            }
            var User = new User()
            {
                FullName = Register.FullName,
                UserName = Register.UserName.ToString(),
                Email = Register.Email.ToString(),
                Password = Register.Password.ToString(),
                PhoneNumber = Register.PhoneNumber.ToString(),
                Avatar = "user",
                City = "user",
                Address = "user",
                Address2 = "user",
                IPAddress = "user",
                IsAdmin = false,
                LastLoginTime = DateTime.Now,
                VerifyToken = ServerSideService.TokenGenerator(40).ToString()
            };
            Context.Users.Add(User);
            Context.SaveChanges();
            return View("RegisterSuccess",User);
        }
        [HttpGet("/Auth/Login")]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Auth/Login")]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(User);
            }
            var user = Context.Users.FirstOrDefault(u => u.Email == login.Email.ToLower() && u.Password == login.Password);
            var Order = Context.Orders.Any(o => o.UserId == user.Id);
            if (user != null)
            {
                if (user.VerifyToken == "")
                {
                    var Claims = new List<Claim>() {
                        new Claim(ClaimTypes.Name,user.UserName,ClaimValueTypes.String),
                        new Claim("Id",user.Id.ToString()),
                        new Claim("isAdmin",user.IsAdmin.ToString(),ClaimValueTypes.Boolean),
                        new Claim("Email",user.Email.ToString()),
                        new Claim("UserName",user.UserName.ToString(),ClaimValueTypes.String),
                        new Claim("FullName",user.FullName.ToString(),ClaimValueTypes.String),
                        new Claim("Avatar",user.Avatar.ToString()),
                        new Claim("HasAnyProductInCheckOutCart",Order.ToString(),ClaimValueTypes.Boolean),
                    };

                    var identityClaims = new ClaimsIdentity(Claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principalClaims = new ClaimsPrincipal(identityClaims);
                    var properties = new AuthenticationProperties()
                    {
                        IsPersistent = login.RememberMe,
                    };

                    HttpContext.SignInAsync(principalClaims, properties);
                    return Redirect("/Home/Index");
                }
                ModelState.AddModelError("Email", "Please Verify Your Account First (Check Your Email)");
                return View();
            }
            ModelState.AddModelError("Email", "Field is Incorrect");
            ModelState.AddModelError("Password", "Field is Incorrect");
            return View();
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/Auth/Login");
        }

    }
}

