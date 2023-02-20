using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class ForgotPasswordController : AuthController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ForgotPasswordController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        
        public IActionResult Index()
        {
            if (isAuth(out _))
            {
                return Redirect("/Home");
            }
            
            return View();
        }

        //[HttpPost]
        //public IActionResult SubmitForgot()
        //{
        //    var token = _userManager.GeneratePasswordResetTokenAsync(user).Result;
        //    var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, token = token }, protocol: HttpContext.Request.Scheme);

        //}
    }
}
