using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class LogoutController : AuthController
    {
        public IActionResult Index()
        {
            if (!isAuth("", out User user))
            {
                return Redirect("/Login");
            }

            HttpContext.Session.Remove("UserId");

            return Redirect("/Login");
        }
    }
}
