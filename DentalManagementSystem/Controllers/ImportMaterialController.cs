using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using System.Drawing.Printing;
using DentalManagementSystem.Utils;

namespace DentalManagementSystem.Controllers
{
    public class ImportMaterialController : AuthController
    {
        ImportMaterialDBContext DB = new ImportMaterialDBContext();
        SystemLogDBContext Log = new SystemLogDBContext();
        MaterialDBContext DBM = new MaterialDBContext();

        // GET: MaterialImport
        public IActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            
            if (!isAuth("/ImportMaterial", out User user))
            {
                return NotFound();
            }

            var query = DB.MaterialImports.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.SupplyName.Contains(search) || u.Amount.ToString().Contains(search) || u.Date.ToString().Contains(search) || u.TotalPrice.ToString().Contains(search));
            }
            ViewData["stt"] = page - 1;
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            var users = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

            return View(users);
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
        public IActionResult Create([Bind("Id, MaterialId, Date, Amount, SupplyName, TotalPrice")]MaterialImport materialImport, int id)
        {
            if (isAuth("/ImportMaterial/Create", out User user))
            {
                TempData["addsuccess"] = "thêm mới thành công";
                materialImport.Date = DateTime.Now;
                //materialImport.SupplyName = DBM.Get(id).Name;
                DB.Add(materialImport);
                Log.Add(new SystemLog
                {
                    CreatedDate = DateTime.Now,
                    OwnerId = user.Id,
                    Content = "người dùng đã thêm mới bệnh án "
                    
                });
                return RedirectToAction(nameof(Index));
            }
            else return NotFound();
        }

        // GET: thay đổi thông tin đơn nhập vật phẩm
        public IActionResult Edit([Bind("Id, MaterialId, Date, Amount, SupplyName, TotalPrice")] MaterialImport materialImport)
        {
            if (!isAuth("/ImportMaterial/Edit", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thay đổi thông tin của bệnh nhân " });
            DB.Update(materialImport);
            TempData["editsuccess"] = "edit thành công";
            return RedirectToAction("Details", new { id = materialImport.Id });
        }

        // POST: thay đổi thông tin đơn nhập vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id, MaterialId, Date, Amount, SupplyName, TotalPrice")] MaterialImport materialImport)
        {
            DB.Update(materialImport);
            TempData["editsuccess"] = "edit thành công";
            return RedirectToAction("Details", new { id = materialImport.Id });
        }


        // xóa đơn nhập vật phẩm
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            if (!isAuth("/ImportMaterial/Delete", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            TempData["Delete messenger"] = "xóa thành công";
            DB.Delete(id);
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã xóa phiếu nhập " });
            
            return RedirectToAction(nameof(Index));
        }

        
    }
}
