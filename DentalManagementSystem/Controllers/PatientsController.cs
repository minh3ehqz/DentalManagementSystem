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
    public class PatientsController : Controller
    {
        PatientDBContext DB = new PatientDBContext();

        // GET: Patients
        public IActionResult Index([Bind("Name,Birthday,Gender,Address,Phone,Email")] Patient patient)
        {
            if (patient.Id != 0 || patient.Name != null || patient.Address != null || patient.Phone != null || patient.Email != null)
            {
                var result = DB.Patients.Where(x => patient.Id != 0 && x.Id == patient.Id
                || patient.Name != null && x.Name.Contains(patient.Name)
                || patient.Address != null && x.Address.Contains(patient.Address)
                || patient.Phone != null && x.Phone.Contains(patient.Phone)
                || patient.Email != null && x.Email.Contains(patient.Email)).ToList();
                return View(result);
            }
            var PatientList = DB.ListAll();
            return View(PatientList);
        }



        // thông tin chi tiết của bệnh nhân
        public IActionResult Details(long id)
        {
            if (id == null)
            {
                return NotFound();
            }
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
                DB.Add(patient);
                return RedirectToAction(nameof(Index));
            }
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
            if (id != patient.Id)
            {
                return NotFound();
            }
            DB.Update(patient);
            return RedirectToAction("Details", new { id = patient.Id });
        }


        // xóa bệnh nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long id)
        {
            DB.Delete(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
