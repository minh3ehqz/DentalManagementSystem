using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Printing;
using Microsoft.EntityFrameworkCore;

namespace DentalManagementSystem.Controllers
{
    public class TimeKeepingController : AuthController
    {
        TimeKeepingDBContext db = new TimeKeepingDBContext();
        public IActionResult Index(string search, int page = 1, int pageSize = 2)
        {
            if (!isAuth("/User/EditProfile", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            
            var query = db.Timekeepings.Where(x => x.UserId == user.Id).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.TimeCheckin == DateTime.Parse(search));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var time = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewData["stt"] = page - 1;
            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewData["uid"] = user.RoleId;
            return View(time);
        }

   
        public IActionResult ResetTimeKeeping(int id)
        {
            if (!isAuth("/User/EditProfile", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            var item = db.get(id);
            item.TimeCheckout = DateTime.Parse("1755-1-1");
            db.Update(item);
            return RedirectToAction("KeepingManager");
        }

        [HttpPost]
        public IActionResult CheckOut()
        {
            if (!isAuth("/User/EditProfile", out User user))
            {
                return NotFound();
            }
            var time = db.Get(user.Id);
            if (time.TimeCheckout.Year == 1755)
                time.TimeCheckout = DateTime.Now;
            db.Update(time);
            return RedirectToAction("Index");
        }

        public IActionResult KeepingManager(string search, int page = 1, int pageSize = 2)
        {
            if (!isAuth("/User/EditProfile", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;


            var query = db.Timekeepings.AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.TimeCheckin == DateTime.Parse(search));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var time = query.Skip((page - 1) * pageSize).Take(pageSize).Include(x=>x.User).ToList();
            ViewData["stt"] = page - 1;
            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;
            ViewData["uid"] = user.RoleId;
            return View(time);
        }
    }
}
