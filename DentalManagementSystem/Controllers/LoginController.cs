using DentalManagementSystem.DAL;
using Microsoft.AspNetCore.Mvc;
using System;

namespace DentalManagementSystem.Controllers
{
    public class LoginController : AuthController
    {
        TimeKeepingDBContext timeCheck = new TimeKeepingDBContext();
        public IActionResult Index()
        {
            if (isAuth("/Home", out _))
            {
                return Redirect("/Home");
            }
            ViewData["LoginError"] = "";
            return View();
        }

        [HttpPost]
        public IActionResult Index(string username, string password)
        {
            if (isAuth("/Home", out _))
            {
                return Redirect("/Home");
            }
            UserDBContext userDBContext = new UserDBContext();
            var user = userDBContext.GetByUsernamePassword(username.Trim(), password.Trim());
            if (user != null)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                if(DateTime.Now.Hour >=8 && DateTime.Now.Hour <=17 && DateTime.Now.DayOfWeek >= DayOfWeek.Monday && DateTime.Now.DayOfWeek <= DayOfWeek.Friday)
                {
                    timeCheck.Add(new Models.Timekeeping
                    {
                        UserId = user.Id,
                        TimeCheckin = DateTime.Now,
                        TimeCheckout = DateTime.Parse("1755-01-01"),
                    });
                }
                return Redirect("/Home");
            }
            ViewData["LoginError"] = "Sai tài khoản hoặc mật khẩu";
            return View();
        }
    }
}
