using Microsoft.AspNetCore.Mvc;
using Shop.Web.Models.ViewModel;
using Shop.Web.Models;
using Shop.Web.Data;
using System.Security.Cryptography;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;

namespace Shop.Web.Controllers
{
    public class AuthController : Controller
    {
        public ShopDBContext Context { get; set; }
        public AuthController(ShopDBContext context)
        {
            Context = context;
        }
        public string GetIP()
        {
            // Get the remote IP address from the HttpContext
            string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();

            // If the IP address is IPv6, extract the IPv4 address
            if (ipAddress.Contains(":"))
            {
                ipAddress = ipAddress.Split(':')[0];
            }

            // Convert IP address to IPv4 format

            // Do something with the IP address...

            return ipAddress;
        }

        public static string TokenGenerator(int length)
        {
            using var rng = new RNGCryptoServiceProvider();
            var bytes = new byte[length];
            rng.GetBytes(bytes);
            return Convert.ToBase64String(bytes);
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
                VerifyToken = TokenGenerator(40).ToString()
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
        public IActionResult Login(LoginViewModel login)
        {
            login.Email = login.Email.ToLower();
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = Context.Users.FirstOrDefault(u => u.Email == login.Email && u.Password == login.Password);

            if (user == null)
            {
                ModelState.AddModelError("Email","User Was Not Found !");
                ModelState.AddModelError("Password","User Was Not Found !");
                return View(login);
            }
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim("IsAdmin",user.IsAdmin.ToString()),
                new Claim("Avatar",user.Avatar.ToString()),
                new Claim("Email",user.Email.ToString()),
            };

            var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties() { IsPersistent = login.RememberMe };
            HttpContext.SignInAsync(principal, properties);

            return RedirectToRoute("/User/Home/Index");
        }

        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToRoute("/Home/Index");
        }

    }
}
