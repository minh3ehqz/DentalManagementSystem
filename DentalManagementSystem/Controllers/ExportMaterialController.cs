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
    public class ExportMaterialController : Controller
    {
        ExportMaterialDBContext DB = new ExportMaterialDBContext();

        // GET: ExportMaterial
        public IActionResult Index(long id, long MaterialId, int Amount, int totalPrice, long PatientRecordId)
        {
            if (id != 0 || MaterialId != null || PatientRecordId != null || Amount != null || totalPrice != null)
            {
                var result = DB.MaterialExports.Where(x => id != 0 && x.Id == id
                || Amount != null && x.Amount.Equals(Amount)    
                || totalPrice != null && x.TotalPrice.Equals(totalPrice)).ToList();
                return View(result);
            }
            var materialExportList = DB.ListAll();
            return View(materialExportList);
        }



        // thông tin chi tiết của đơn xuất vật phẩm
        public IActionResult Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var materialExport = DB.Get(id);
            return View(materialExport);
        }

        // GET: thêm mới đơn xuất vật phẩm
        public IActionResult Create()
        {
            return View();
        }

        // POST: thêm mới đơn xuất vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, Materialid, Amount, TotalPrice, PatientRecordId")] MaterialExport materialExport)
        {
            if (ModelState.IsValid)
            {
                DB.Add(materialExport);
                return RedirectToAction(nameof(Index));
            }
            return View(materialExport);
        }

        // GET: thay đổi thông tin đơn xuất vật phẩm
        public IActionResult Edit(long id)
        {
            var materialExport = DB.Get(id);
            if (materialExport == null)
            {
                return NotFound();
            }
            return View(materialExport);
        }

        // POST: thay đổi thông tin đơn xuất vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id, Materialid, Amount, TotalPrice, PatientRecordId")] MaterialExport materialExport)
        {
            if (id != materialExport.Id)
            {
                return NotFound();
            }
            DB.Update(materialExport);
            return RedirectToAction("Details", new { id = materialExport.Id });
        }


        // xóa đơn xuất vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            DB.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
