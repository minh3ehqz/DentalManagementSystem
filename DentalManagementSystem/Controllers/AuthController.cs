using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public abstract class AuthController : Controller
    {
        public bool isAuth()
        {
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
