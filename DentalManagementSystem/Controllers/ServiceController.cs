using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;

namespace DentalManagementSystem.Controllers
{
    public class ServiceController : Controller
    { 
        ServiceDBContext DB = new ServiceDBContext();


        // GET: Services
        public IActionResult Index(long? id, string name, int unit, int marketprice, int price)
        {
            if (id != 0 || name != null || unit != null || marketprice != null || price != null)
            {
                var result = DB.Service.Where(s => !(id.HasValue && s.Id != id.Value
            && (name == null || !s.Name.Contains(name))
            && (unit == null || !s.Unit.Contains(unit))
            && (marketprice == null || !s.MarketPrice.Equals(marketprice))
            && (price == null || !s.price.Equals(price)))).ToList();

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
        public IActionResult Create([Bind("Id,Name,Unit,MarketPrce,Price")] Service service)
        {
            if (ModelState.IsValid)
            {
                DB.Add(service);
                return RedirectToAction(nameof(Index));
            }

            return View(service);
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
        public IActionResult Edit(long id, [Bind("Id,Name,Unit,MarketPrce,Pricey")] Service service  )
        {
            if (id != service.Id)
                DB.Update(service);
            return RedirectToAction("Details", new { id = service.Id });
        }

        // xóa service
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            DB.Delete(id);
            return RedirectToAction(nameof(Index));

        }

    }
}
