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
using System.Net;
using DentalManagementSystem.Utils;

namespace DentalManagementSystem.Controllers
{
    public class ServiceController : AuthController
    {
        ServiceDBContext DB = new ServiceDBContext();
        SystemLogDBContext Log = new SystemLogDBContext();

        // GET: Services
      
        public IActionResult Index(string search, int page = 1, int pageSize = 2)
        {
           
            if (!isAuth("/Service", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;

            var query = DB.Services.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Name.Contains(search));
            }

            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);

            var services = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            ViewData["stt"] = page - 1;
            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(services);
        }

        //thông tin chi tiết service
        public IActionResult Details(long id)
        {
            if (!isAuth("/Service", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            var service = DB.Get(id);
            return View(service);
        }

        // GET: thêm mới service
        public IActionResult Create()
        {
            return View();
        }

        // POST: thêm mới service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Name,Unit,MarketPrice,Price")] Service service)
        {
            if (!isAuth("/Service/Create",out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            DB.Add(service);
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thêm một dịch vụ mới là "+service.Name+"" });
            return RedirectToAction(nameof(Index));


        }

        // GET: thay đổi thông tin service
        public IActionResult Edit(long id)
        {
            if (!isAuth("/Service", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            var service = DB.Get(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        // POST: thay đổi thông tin service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Name,Unit,MarketPrice,Price")] Service editService)
        {
            if (!isAuth("/Service/Edit",out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            Service service;
            service = DB.Services.FirstOrDefault(s => s.Id == editService.Id);
            if (service != null)
            {
                service.Name = editService.Name;
                service.Unit = editService.Unit;
                service.MarketPrice = editService.MarketPrice;
                service.Price = editService.Price;
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thay đổi thông tin dịch vụ " + service.Name + "" });
            }
            DB.SaveChanges();
            return RedirectToAction("Details", new { id = editService.Id });
        }





        // xóa service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long[] selectedValues)
        {
            if (!isAuth("/Service/Delete",out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thay đổi thông tin dịch vụ " + DB.Get(id).Name + "" });
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));

        }
        //tìm service
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Search(long Id, String Name, int Unit, int MarketPrice, int Price, String Reset)
        //{
        //    List<Service> services = DB.ListAll();

        //    if (!String.IsNullOrEmpty(Reset)) return View("Index", services);

        //    if (Id != 0) services = services.Where(s => s.Id == Id).ToList();

        //    if (!String.IsNullOrEmpty(Name)) services = services.Where(s => s.Name.Contains(Name)).ToList();

        //    if (Unit != 0) services = services.Where(s => s.Unit == Unit).ToList();

        //    if (MarketPrice != 0) services = services.Where(s => s.MarketPrice == MarketPrice).ToList();

        //    if (Price != 0) services = services.Where(s => s.Price == Price).ToList();

        //    return View("Index", services);
        //}

    }
}
