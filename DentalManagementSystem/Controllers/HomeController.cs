using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DentalManagementSystem.Controllers
{
    public class HomeController : AuthController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }


        public IActionResult Index()
        {
            if (!isAuth("/Home/Index", out User user))
            {
                return Redirect("/Login");
            }
            if (user.RoleId == 3) return Redirect("/SystemLogs");
            if (user.RoleId == 4) return Redirect("/Patients");
            if (user.RoleId == 5) return Redirect("/Patients");
            if (user.RoleId == 6) return Redirect("/Patients");
            if (user.RoleId == 7) return Redirect("/Material");
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;


            return View(user);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}