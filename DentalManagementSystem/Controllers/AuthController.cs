using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public abstract class AuthController : Controller
    {
        public bool isAuth(out User user)
        {
            UserDBContext db = new UserDBContext();
            if (HttpContext.Session.GetString("UserId") != null)
            {
                user = db.Get(long.Parse(HttpContext.Session.GetString("UserId")));
                return true;
            }
            else
            {
                user = null;
                return false;
            }
        }
    }
}
