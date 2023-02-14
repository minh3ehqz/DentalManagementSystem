using DentalManagementSystem.DAL;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class LoginController : AuthController
    {
        public IActionResult Index()
        {
            if (isAuth())
            {
                return Redirect("/Home");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (isAuth())
            {
                return Redirect("/Home");
            }
            UserDBContext DB = new UserDBContext();
            var user = DB.GetByUsernamePassword(username, password);
            if (user != null)
            {
                return Redirect("/Home");
            }
            ViewData["LoginError"] = "Sai tài khoản hoặc mật khẩu";
            return View();
        }
    }
}
