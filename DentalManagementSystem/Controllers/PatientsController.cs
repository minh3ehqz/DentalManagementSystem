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
        public IActionResult Index(string textSearch, int page = 1)
        {
            if (!isAuth("/Patients",out User user))
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
        // POST: thêm mới bệnh nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Birthday,Gender,Address,Phone,Email,BodyPrehistory,TeethPrehistory,Status,IsDeleted")] Patient patient)
        {
            if(isAuth("/Patients/",out User user))
            {
                TempData["addsuccess"] = "thêm mới thành công";
                patient.Trim();
                DB.Add(patient);
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thêm mới bệnh nhân " +
                    patient.Name+" có sô điện thoại là "+patient.Phone+" và email là "+patient.Email });
                return RedirectToAction(nameof(Index));
            }else return NotFound();
            
        }
        // thông tin chi tiết của bệnh nhân
        public IActionResult Details(long id)
        {
            if (!isAuth("/Patients/Details", out User user))
            {
                return NotFound();
            }
            var patient = DB.Get(id);
            return View(patient);
        }
        // GET: thay đổi thông tin bênh nhân


        // POST: thay đổi thông tin bênh nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(long id, [Bind("Id,Name,Birthday,Gender,Address,Phone,Email,BodyPrehistory,TeethPrehistory,Status,IsDeleted")] Patient patient)
        {
            if (!isAuth("/Patients/Edit", out User user))
            {
                return NotFound();
            }
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thay đổi thông tin của bệnh nhân "+patient.Name+ " có sđt là " + patient.Phone + " và email là " + patient.Email + "" });
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
            if (!isAuth("/Patients/Delete",out User user))
            {
                return NotFound();
            }
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã xóa bệnh nhân " + DB.Get(id).Name + " có sđt là " + DB.Get(id).Phone + " và email là " + DB.Get(id).Email + "" });
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        public IActionResult checkEmailPhone(string email, string phone)
        {
            var checkEmail = DB.GetPatientsByEmail(email);
            var checkPhone = DB.GetPatientsByPhone(phone);
            if (checkEmail != null || checkPhone != null)
            {
                string result = "";
                if (checkEmail != null) result += "email ";
                if (checkPhone != null) {
                    if (!result.Equals("")) result += "và ";
                    result += "phone ";
                } 
                return Ok(result + "đã tồn tại");
            }
            else
            {
                return Ok("Valid");
            }
        }
        
    }
}
