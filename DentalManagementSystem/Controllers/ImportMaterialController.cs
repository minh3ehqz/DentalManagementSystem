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
    public class ImportMaterialController : AuthController
    {
        ImportMaterialDBContext DB = new ImportMaterialDBContext();

        // GET: MaterialImport
        public IActionResult Index(long id, long MaterialId, DateTime Date, int Amount, String name, int totalPrice)
        {
            if (id != null || name != null || MaterialId != null || Date != null || Amount != null || totalPrice != null)
            {
                var result = DB.MaterialImports.Where(
                    x => id != null && x.Id == id
                || name != null && x.SupplyName.Contains(name)
                || Date != null && x.Date.ToString().Contains(Date.ToString())
                || Amount != null && x.Amount.Equals(Amount)
                || totalPrice != null && x.TotalPrice.Equals(totalPrice)
                || x.IsDeleted != true).ToList();
                return View(result);
            }
            var materialImportList = DB.ListAll();
            return View(materialImportList);
        }



        // thông tin chi tiết của đơn nhập vật phẩm
        public IActionResult Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var materialImport = DB.Get(id);
            return View(materialImport);
        }

        // GET: thêm mới đơn nhập vật phẩm
        public IActionResult Create()
        {
            return View();
        }

        // POST: thêm mới đơn nhập vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, MaterialId, Date, Amount, SupplyName, TotalPrice")]MaterialImport materialImport)
        {
            if (ModelState.IsValid)
            {
                TempData["addsuccess"] = "thêm mới thành công";
                materialImport.Date = DateTime.Now;
                DB.Add(materialImport);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: thay đổi thông tin đơn nhập vật phẩm
        public IActionResult Edit(long id)
        {
            var materialImport = DB.Get(id);
            if (materialImport == null)
            {
                return NotFound();
            }
            return View(materialImport);
        }

        // POST: thay đổi thông tin đơn nhập vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id, MaterialId, Date, Amount, SupplyName, TotalPrice")] MaterialImport materialImport)
        {
            if (id != materialImport.Id)
            {
                return NotFound();
            }
            DB.Update(materialImport);
            return RedirectToAction("Details", new { id = materialImport.Id });
        }


        // xóa đơn nhập vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            DB.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
