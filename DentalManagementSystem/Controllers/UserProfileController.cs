using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class UserProfileController : AuthController
    {
        public IActionResult Index()
        {
            if (!isAuth(out User user))
            {
                return Redirect("/Login");
            }

            return View(user);
        }

        public IActionResult PasswordChange()
        {
            if (!isAuth(out _))
            {
                return Redirect("/Login");
            }
            
            return View();
        }

        [HttpPost]
        public IActionResult PasswordChange(string oldPassword, string newPassword, string newPasswordConfirm)
        {
            if (!isAuth(out User user))
            {
                return Redirect("/Login");
            }

            if (oldPassword != user.Password)
            {
                ViewData["Error"] = "Bạn đã nhập sai mật khẩu cũ";
            }
            else if (newPassword != newPasswordConfirm)
            {
                ViewData["Error"] = "Bạn đã nhập 2 mật khẩu không trùng khớp";
            }
            else
            {
                user.Password = newPassword;
            }
            
            UserDBContext userDB = new UserDBContext();
            userDB.Update(user);

            return View();
        }
    }
}
