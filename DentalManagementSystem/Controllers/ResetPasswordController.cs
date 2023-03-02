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
            
            if (HttpContext.Session.GetString(UserId + "-ResetPassword") == ValidateToken)
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
