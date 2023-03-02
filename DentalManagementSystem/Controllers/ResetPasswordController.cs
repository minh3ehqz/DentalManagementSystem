using DentalManagementSystem.DAL;
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
            UserDBContext userDBContext = new UserDBContext();
            if (HttpContext.Session.GetString(User + "-ResetPassword") == ValidateToken)
            {
                return View();
            }
            return View("ValidateError");
        }

        public IActionResult ValidateError()
        {
            return View();
        }
    }
}
