using Microsoft.AspNetCore.Mvc;

namespace Shop.Web.Controllers
{
    public class AuthController : Controller
    {
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
        public IActionResult Index()
        {
            return View();
        }
    }
}
