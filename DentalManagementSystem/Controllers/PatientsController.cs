using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
namespace DentalManagementSystem.Controllers
{
    public class PatientsController : AuthController
    {
        PatientDBContext DB = new PatientDBContext();

        // GET: Patients
        public IActionResult Index(long? id, String name, String Birthday, String address, String phone, String email)
        {
            if (id != null || Birthday != null || name != null || address != null || phone != null || email != null)
            {
                var result = DB.Patients.Where(p => (!id.HasValue || p.Id == id.Value)
                && p.Name.Contains((name ?? ""))
                && p.Birthday.ToString().Contains((Birthday ?? ""))
                && p.Address.Contains((address ?? ""))
                && p.Phone.Contains((phone ?? ""))
                && p.Email.Contains((email ?? ""))).ToList();

                return View(result);
            }
            var PatientList = DB.ListAll();
            return View(PatientList);
        }


        // thông tin chi tiết của bệnh nhân
        public IActionResult Details(long id)
        {
            var patient = DB.Get(id);
            return View(patient);
        }


        // GET: thêm mới bệnh nhân
        public IActionResult Create()
        {
            return View();
        }

        // POST: thêm mới bệnh nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Birthday,Gender,Address,Phone,Email,BodyPrehistory,TeethPrehistory,Status,IsDeleted")] Patient patient)
        {
            if (ModelState.IsValid)
            {
                TempData["addsuccess"] = "thêm mới thành công";
                patient.Trim();
                DB.Add(patient);
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        // GET: thay đổi thông tin bênh nhân
        public IActionResult Edit(long id)
        {
            var patient = DB.Get(id);
            if (patient == null)
            {
                return NotFound();
            }
            return View(patient);
        }

        // POST: thay đổi thông tin bênh nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Name,Birthday,Gender,Address,Phone,Email,BodyPrehistory,TeethPrehistory,Status,IsDeleted")] Patient patient)
        {
            patient.Trim();
            DB.Update(patient);
            TempData["editsuccess"] = "edit thành công";
            return RedirectToAction("Details", new { id = patient.Id });
        }


        // xóa bệnh nhân
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
