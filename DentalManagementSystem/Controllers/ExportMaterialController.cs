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
    public class ExportMaterialController : AuthController
    {
        ExportMaterialDBContext DB = new ExportMaterialDBContext();

        // GET: ExportMaterial
        public IActionResult Index(long? id, long? MaterialId, int? Amount, int? totalPrice, long? PatientRecordId)
        {
            if (id != null || MaterialId != null || PatientRecordId != null || Amount != null || totalPrice != null)
            {
                var result = DB.MaterialExports.Where(x => (!id.HasValue || x.Id == id.Value)
                && x.Amount == Amount    
                && x.TotalPrice == totalPrice
                && x.PatientRecordId == PatientRecordId).ToList();
                return View(result);
            }
            var materialExportList = DB.ListAll();
            return View(materialExportList);
        }

        // Details information of a record
        public IActionResult Details(long id)
        {           
            var materialExport = DB.Get(id);
            return View(materialExport);
        }

        // get: Add 1 record
        public IActionResult Create()
        {
            return View();
        }

        // POST: Add 1 record 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id, MaterialId, Amount, TotalPrice, PatientRecordId, IsDeleted")] MaterialExport materialExport)
        {						
			TempData["addsuccess"] = "thêm mới thành công"; 
		    DB.Add(materialExport);
            return RedirectToAction(nameof(Index));                        
        }

        // GET: Change information of a record
        public IActionResult Edit(long id)
        {
            var materialExport = DB.Get(id);
            if (materialExport == null)
            {
                return NotFound();
            }
            else
            return View(materialExport);
        }

        // POST: Change information of a record
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id, MaterialId, Amount, TotalPrice, PatientRecordId, IsDeleted")] MaterialExport materialExport)
        {         
				DB.Update(materialExport);
				TempData["editsuccess"] = "edit thành công";
				return RedirectToAction("Details", new { id = materialExport.Id });			
		}


        // Delete a record 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
			TempData["Delete messenger"] = "xóa thành công";
			DB.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
