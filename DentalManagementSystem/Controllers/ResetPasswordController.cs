using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class ResetPasswordController : AuthController
    {
        public IActionResult Index([FromQuery] string UserId, [FromQuery] string ValidateToken)
        {
            if (isAuth(out _))
            {
                return Redirect("/Home");
            }
            if (HttpContext.Session.GetString(UserId + "-ResetPassword") == ValidateToken)
            {
                ViewData["UserId"] = UserId;
                return View();
            }
            return View("ValidateError");
        }

        [HttpPost]
        public IActionResult Reset(long UserId, string password)
        {
            UserDBContext userDB = new UserDBContext();
            User user = userDB.Get(UserId);
            user.Password = password;

			userDB.Update(user);

            ViewData["Success"] = "Bạn đã khôi phục mật khẩu thành công! Xin mời bạn đăng nhập vào hệ thống";

			return View("Index");
        }

        public IActionResult ValidateError()
        {
            return View();
        }
    }
}
