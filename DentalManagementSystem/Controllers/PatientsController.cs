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
        SystemLogDBContext Log = new SystemLogDBContext();
        PatientDBContext DB = new PatientDBContext();

        // GET: Patients
        public IActionResult Index(long? id, String name, String Birthday, String address, String phone, String email, String gender)
        {
            var checkgender = (gender ?? "");
            TempData["id"] = (id ?? null);
            TempData["name"] = (name ?? "");
            TempData["Birthday"] = (Birthday ?? "");
            TempData["address"] = (address ?? "");
            TempData["phone"] = (phone ?? "");
            TempData["email"] = (email ?? "");
            TempData["gender"] = checkgender;
            if (id != null || Birthday != null || name != null || address != null || phone != null || email != null || gender != null)
            {

                var result = DB.Patients.Where(p => (!id.HasValue || p.Id == id.Value)
                && p.Name.Contains((name ?? ""))
                && p.Birthday.ToString().Contains((Birthday ?? ""))
                && p.Address.Contains((address ?? ""))
                && p.Phone.Contains((phone ?? ""))
                && (p.Gender ? "m" : "f").Contains(checkgender)
                && p.Email.Contains((email ?? ""))).ToList();

                return View(result);
            }
            var PatientList = DB.ListAll();
            return View(PatientList);
        }

        // POST: thêm mới bệnh nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Birthday,Gender,Address,Phone,Email,BodyPrehistory,TeethPrehistory,Status,IsDeleted")] Patient patient)
        {
            TempData["addsuccess"] = "thêm mới thành công";
            patient.Trim();
            DB.Add(patient);
            //Log.Add(new SystemLog { CreatedDate = DateTime.Now,OwnerId=1,Content="người dùng đã thêm mới bệnh nhân" });
            return RedirectToAction(nameof(Index));
        }
        // thông tin chi tiết của bệnh nhân
        public IActionResult Details(long id)
        {
            var patient = DB.Get(id);
            return View(patient);
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
        public IActionResult Delete(long[] selectedValues)
        {
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

    }
}
