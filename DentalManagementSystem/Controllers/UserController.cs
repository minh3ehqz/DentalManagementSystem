using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using Microsoft.IdentityModel.Tokens;

namespace DentalManagementSystem.Controllers
{
    public class UserController : AuthController
    {
        UserDBContext DB = new UserDBContext();
        RoleDBContext RoleDB = new RoleDBContext();


        // GET: Users
        public IActionResult Index()
        {
            var UserList = DB.Users.Include(u => u.Role).ToList();

            return View(UserList);
        }

        // thông tin chi tiết User
        public IActionResult Details(long id)
        {
            var user = DB.Users.Include(u => u.Role).FirstOrDefault(u => u.Id == id);

            return View(user);
        }

        // GET: thêm mới user
        public IActionResult Create()
        {
            ViewBag.Roles = RoleDB.Roles.ToList();
            return View();
        }

        // POST: thêm mới User
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Username,FullName,Password,Birthday,Phone,Salary,RoleId,Enable,Email")] User user)
        {


            DB.Add(user);
            return RedirectToAction(nameof(Index));
            return View(user);
        }

        // GET: thay đổi thông tin user
        public IActionResult Edit(long id)
        {
            var user = DB.Get(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewBag.Roles = RoleDB.Roles.ToList();
            return View(user);
        }

        // POST: thay đổi thông tin user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Username,FullName,Password,Birthday,Phone,Salary,RoleId,Enable,Email")] User editUser)
        {
            User user = DB.Users.FirstOrDefault(s => s.Id == editUser.Id);
            if (user != null)
            {
                user.Username = editUser.Username.Trim();
                user.FullName = editUser.FullName;
                user.Birthday = editUser.Birthday;
                user.Phone = editUser.Phone;
                user.Salary = editUser.Salary;
                user.RoleId = editUser.RoleId;
            }
            DB.SaveChanges();
            return RedirectToAction("Details", new { id = editUser.Id });
        }

        // xóa user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long[] selectedValues)
        {
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));

        }




        //tìm user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(long Id, String UserName, String FullName, DateTime Birthday, String Phone, int Salary, String Role, String Email, String Reset)
        {
            var users = DB.Users.Include(u => u.Role).ToList();


            var bday = Birthday.ToString();

            if (!String.IsNullOrEmpty(Reset)) return View("Index", users);

            if (Id != 0) users = users.Where(s => s.Id == Id).ToList();

            if (!String.IsNullOrEmpty(UserName)) users = users.Where(s => s.Username.Contains(UserName.Trim())).ToList();

            if (!String.IsNullOrEmpty(FullName)) users = users.Where(s => s.FullName.Contains(FullName.Trim())).ToList();

            if (!String.IsNullOrEmpty(Phone)) users = users.Where(s => s.Phone.Contains(Phone.Trim())).ToList();

            if (!Birthday.ToString().Trim().Equals("1/1/0001 12:00:00 AM".Trim())) users = users.Where(s => s.Birthday.Equals(Birthday)).ToList();

            if (Salary != 0) users = users.Where(s => s.Salary == Salary).ToList();

            if (!String.IsNullOrEmpty(Email)) users = users.Where(s => s.Email.Contains(Email.Trim())).ToList();

            if (!String.IsNullOrEmpty(Role)) users = DB.Users.Include(u => u.Role).Where(s => s.Role.Name.Contains(Role.Trim())).ToList();



            return View("Index", users);
        }

        public IActionResult checkEmailPhone(string email, string phone)
        {
            var checkEmail = DB.GetUsersByEmail(email);
            var checkPhone = DB.GetUsersByPhone(phone);
            if (checkEmail != null || checkPhone != null)
            {
                string result = "";
                if (checkEmail != null) result += "email ";
                if (checkPhone != null)
                {
                    if (!result.Equals("")) result += "và ";
                    result += "phone ";
                }
                return Ok(result + "đã tồn tại");
            }
            else
            {
                return Ok("Valid");
            }
        }
    }
}

