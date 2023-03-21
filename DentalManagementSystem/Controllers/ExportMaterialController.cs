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
        //public IActionResult Index(long? id, long? MaterialId, int? Amount, int? totalPrice, long? PatientRecordId)
        //{
        //    if (id != null || MaterialId != null || PatientRecordId != null || Amount != null || totalPrice != null)
        //    {
        //        var result = DB.MaterialExports.Where(x => (!id.HasValue || x.Id == id.Value)
        //        && x.Amount == Amount    
        //        && x.TotalPrice == totalPrice
        //        && x.PatientRecordId == PatientRecordId).ToList();
        //        return View(result);
        //    }
        //    var materialExportList = DB.ListAll();
        //    return View(materialExportList);
        //}

        public IActionResult Index(string textSearch, int page = 1)
        {
			if (!isAuth("/ExportMaterial", out User user))
			{
				return NotFound();
			}
			ViewData["searchContent"] = textSearch;
            int count = DB.ListAll((string)ViewData["searchContent"]).Count();
            ViewData["thisPage"] = page;
            ViewData["stt"] = page - 1;
            ViewData["numberOfPage"] = count % 10 == 0 ? (count / 10) : (count / 10) + 1;
            var list = DB.ListInPage(page, (string)ViewData["searchContent"]);
            return View(list);
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
        public IActionResult Create([Bind("Id, MaterialId, Amount, TotalPrice, PatientRecordId, Date, IsDeleted")] MaterialExport materialExport)
        {
			if (isAuth("/ExportMaterial/Create", out User user)) { 
				TempData["addsuccess"] = "thêm mới thành công"; 
		    DB.Add(materialExport);
		    return RedirectToAction(nameof(Index));
			}
			else return NotFound();
		}

        // GET: Change information of a record
        public IActionResult Edit(long id)
        {
			if (!isAuth("/ExportMaterial/Edit", out User user))
			{
				return NotFound();
			}
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
        public IActionResult Edit(long id, [Bind("Id, MaterialId, Amount, TotalPrice, PatientRecordId, Date, IsDeleted")] MaterialExport materialExport)
        {
			if (!isAuth("/ExportMaterial/Edit", out User user))
			{
				return NotFound();
			}
			DB.Update(materialExport);
				TempData["editsuccess"] = "edit thành công";
				return RedirectToAction(nameof(Index));			
		}


        // Delete a record 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
			if (!isAuth("/ExportMaterial/Delete", out User user))
			{
				return NotFound();
			}
			DB.Delete(id);
			TempData["Delete messenger"] = "xóa thành công";			
            return RedirectToAction(nameof(Index));
        }

    }
}
