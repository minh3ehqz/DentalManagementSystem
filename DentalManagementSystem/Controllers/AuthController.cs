using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public abstract class AuthController : Controller
    {
        public bool isAuth(string UrlPath, out User user)
        {
            UserDBContext db = new UserDBContext();
            RolePermissionMapDBContext RolePermissionDB = new RolePermissionMapDBContext();
            var PermissionList = RolePermissionDB.ListAll();
            if (HttpContext.Session.GetString("UserId") != null)
            {
                user = db.Get(long.Parse(HttpContext.Session.GetString("UserId")));
                if (user == null)
                {
                    return false;
                }
                ViewData["FullName"] = user.FullName;
                ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
                ViewData["Email"] = user.Email;
                ViewData["UserId"] = user.Id;
                var TempUser = user;
                if (UrlPath != "" && !PermissionList.Any(x => x.Permission.Name == UrlPath && x.RoleId == TempUser.RoleId))
                {
                    return false;
                }
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
