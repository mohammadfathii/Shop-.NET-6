

namespace Shop.Web.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Path.StartsWithSegments("/admin"))
            {
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.Redirect("/Auth/Login");
                }else if(httpContext.User.HasClaim("IsAdmin","False"))
                {
                    httpContext.Response.Redirect("/Home");
                }
            }
            else if (httpContext.Request.Path.StartsWithSegments("/user"))
            {
                if (!httpContext.User.Identity.IsAuthenticated)
                {
                    httpContext.Response.Redirect("/Auth/Login");
                }
            }
            
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }

 
}
