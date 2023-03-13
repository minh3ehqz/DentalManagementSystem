using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class ForgotPasswordController : AuthController
    {        
        public IActionResult Index()
        {
            if (isAuth(out _))
            {
                return Redirect("/Home");
            }
            ViewData["ForgotError"] = "";
            return View();
        }
        
        [HttpPost]
        public IActionResult Index(string email)
        {
            UserDBContext userDBContext = new UserDBContext();
            User user = userDBContext.GetUsersByEmail(email);
            if (user == null)
            {
                ViewData["ForgotError"] = "Email không tồn tại trong hệ thống";
                return View();
            }

            string ValidateToken = StringHelper.GenerateToken();
            HttpContext.Session.SetString(user.Id + "-ResetPassword", ValidateToken);
            string UserEmail = user.Email.Trim();
            string EmailResult = EmailHelper.Send(UserEmail, "Khôi phục mật khẩu cho hệ thống", "Chào bạn, đường dẫn khôi phục hệ thống của bạn là: https://localhost:7006/ResetPassword?UserId=" + user.Id + "&ValidateToken=" + ValidateToken);
            if (EmailResult == "OK")
            {
                ViewData["Success"] = "Chúng tôi đã gửi cho bạn một email khôi phục mật khẩu, vui lòng làm theo hướng dẫn để khôi phục mật khẩu!";
            }
            else
            {
                ViewData["ForgotError"] = "Đã xảy ra lỗi khi chúng tôi thử gửi email cho bạn, vui lòng thử lại sau!";
            }
            return View();
        }

    }
}
