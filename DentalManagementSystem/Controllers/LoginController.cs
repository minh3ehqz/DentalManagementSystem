using DentalManagementSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class LoginController : AuthController
    {
        public IActionResult Index()
        {
            if (isAuth(out _))
            {
                return Redirect("/Home");
            }
            ViewData["LoginError"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (isAuth(out _))
            {
                return Redirect("/Home");
            }
            UserDBContext userDBContext = new UserDBContext();
            var user = userDBContext.GetByUsernamePassword(username.Trim(), password.Trim());
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                return Redirect("/Home");
            }
            ViewData["LoginError"] = "Sai tài khoản hoặc mật khẩu";
            return View();
        }
    }
}
