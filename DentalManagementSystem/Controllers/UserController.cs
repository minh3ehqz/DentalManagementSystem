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
        SystemLogDBContext Log = new SystemLogDBContext();

        // GET: Users
         public IActionResult Index(string search, int page = 1, int pageSize = 10)
            {
            var UserList = DB.Users.Include(u => u.Role).ToList();
            if (!isAuth("/User/Index", out User user))
                {
                    return NotFound();
                }

                var query = DB.Users.Include(u => u.Role).AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(u => u.Username.Contains(search) || u.FullName.Contains(search) || u.Phone.Contains(search) || u.Email.Contains(search));
                }

                var totalItems = query.Count();
                var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

                var users = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                ViewBag.Search = search;
                ViewBag.Page = page;
                ViewBag.PageSize = pageSize;
                ViewBag.TotalPages = totalPages;

                return View(users);

            }

            return View(user);
        }
        
        // Chỉnh sửa profile
        public IActionResult EditProfile()
        {
            if (!isAuth("/User/EditProfile", out User user))
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost]
        public IActionResult ChangePassword(string OldPassword, string NewPassword)
		{
			if (!isAuth("/User/EditProfile", out User user))
			{
				return NotFound();
			}

            if (user.Password != OldPassword.Trim())
            {
                ViewData["error-message"] = "Bạn nhập mật khẩu cũ không đúng";
				return View("EditProfile", user);
			}

            user.Password = NewPassword;
            UserDBContext UserDbContext = new UserDBContext();
            UserDbContext.Update(user);
            ViewData["success-message"] = "Bạn đã đổi mật khẩu thành công";

			return View("EditProfile", user);
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
            if (!isAuth("/User/Create",out User logUser))
            {
                return NotFound();
            }

            Log.Add(new SystemLog
            {
                CreatedDate = DateTime.Now,
                OwnerId = logUser.Id,
                Content = "người dùng đã thêm mới nhân viên " +
                    "" + user.FullName + " có sô điện thoại là " + user.Phone + " và email là " + user.Email + ""
            });
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
            if (!isAuth("/User/Edit",out User logUser))
            {
                return NotFound();
            }
            
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
            Log.Add(new SystemLog
            {
                CreatedDate = DateTime.Now,
                OwnerId = logUser.Id,
                Content = "người dùng đã thay đổi thông tin nhân viên " +
                    "" + DB.Get(id).FullName + " có sô điện thoại là " + DB.Get(id).Phone + " và email là " + DB.Get(id).Email + ""
            });
            DB.SaveChanges();
            return RedirectToAction("Details", new { id = editUser.Id });
        }

        // xóa user
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long[] selectedValues)
        {
            if (!isAuth("/User/Delete",out User logUser))
            {
                return NotFound();
            }
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                Log.Add(new SystemLog
                {
                    CreatedDate = DateTime.Now,
                    OwnerId = logUser.Id,
                    Content = "người dùng đã xóa nhân viên " +
                     "" + DB.Get(id).FullName + " có sô điện thoại là " + DB.Get(id).Phone + " và email là " + DB.Get(id).Email + ""
                });
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
                if (checkEmail != null) result += "E0mail ";
                if (checkPhone != null)
                {
                    if (!result.Equals("")) result += "và ";
                    result += "số điện thoại ";
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

