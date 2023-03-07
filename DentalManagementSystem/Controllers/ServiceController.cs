﻿using System;
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

namespace DentalManagementSystem.Controllers
{
    public class ServiceController : AuthController
    {
        ServiceDBContext DB = new ServiceDBContext();
        SystemLogDBContext Log = new SystemLogDBContext();

        // GET: Services
        public IActionResult Index(long? id, string name, int unit, int marketprice, int price)
        {
            if (id != 0 && name != null && unit != null && marketprice != null && price != null)
            {
                var result = DB.Services.Where(s => id != 0 && s.Id == id
            && (name != null && s.Name.Contains(name))
            && (marketprice != null && !s.MarketPrice.Equals(marketprice))
            && (price != null && !s.Price.Equals(price))).ToList();

                return View(result);
            }
            var ServiceList = DB.ListAll();
            return View(ServiceList);
        }

        //thông tin chi tiết service
        public IActionResult Details(long id)
        {
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
            if (!isAuth(out User user))
            {
                return NotFound();
            }
            DB.Add(service);
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thêm một dịch vụ mới là "+service.Name+"" });
            return RedirectToAction(nameof(Index));


        }

        // GET: thay đổi thông tin service
        public IActionResult Edit(long id)
        {
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
            if (!isAuth(out User user))
            {
                return NotFound();
            }
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
            if (!isAuth(out User user))
            {
                return NotFound();
            }
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thay đổi thông tin dịch vụ " + DB.Get(id).Name + "" });
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));

        }
        //tìm service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Search(long Id, String Name, int Unit, int MarketPrice, int Price, String Reset)
        {
            List<Service> services = DB.ListAll();

            if (!String.IsNullOrEmpty(Reset)) return View("Index", services);

            if (Id != 0) services = services.Where(s => s.Id == Id).ToList();

            if (!String.IsNullOrEmpty(Name)) services = services.Where(s => s.Name.Contains(Name)).ToList();

            if (Unit != 0) services = services.Where(s => s.Unit == Unit).ToList();

            if (MarketPrice != 0) services = services.Where(s => s.MarketPrice == MarketPrice).ToList();

            if (Price != 0) services = services.Where(s => s.Price == Price).ToList();

            return View("Index", services);
        }

    }
}
