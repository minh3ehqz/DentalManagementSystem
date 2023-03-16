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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Drawing.Printing;

namespace DentalManagementSystem.Controllers
{
    public class PatientsController : AuthController
    {
        SystemLogDBContext Log = new SystemLogDBContext();
        PatientDBContext DB = new PatientDBContext();
        PatientRecordDBContext DBRecord = new PatientRecordDBContext();
        // GET: Patients
        public IActionResult Index(string textSearch, int page = 1)
        {
            if (!isAuth("/Patients",out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;

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
            if(isAuth("/Patients/Create",out User user))
            {
                ViewData["FullName"] = user.FullName;
                ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
                ViewData["Email"] = user.Email;
                TempData["addsuccess"] = "thêm mới thành công";

                patient.Trim();
                DB.Add(patient);
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thêm mới bệnh nhân " +
                    patient.Name+" có sô điện thoại là "+patient.Phone+" và email là "+patient.Email });
                return RedirectToAction(nameof(Index));
            }else return NotFound();
            
        }
        // thông tin chi tiết của bệnh nhân
        public IActionResult Details(long id, string search, int page = 1, int pageSize = 10)
        {
            if (!isAuth("/Patients/Details", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;

            var query = DB.PatientRecords.Where(x => x.PatientId==id).AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(u => u.Causal.Contains(search) || u.Debit.Contains(search) || u.Date.ToString().Contains(search) || u.Diagnostic.Contains(search));
            }

            ViewData["stt"] = page - 1;
            var totalItems = query.Count();
            var totalPages = (int)Math.Ceiling((decimal)totalItems / pageSize);
            var records = query.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            ViewData["records"] = records;
            ViewBag.Search = search;
            ViewBag.Page = page;
            ViewBag.PageSize = pageSize;
            ViewBag.TotalPages = totalPages;

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
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thay đổi thông tin của bệnh nhân "+patient.Name+ " có sđt là " + patient.Phone + " và email là " + patient.Email + "" });
            patient.Trim();
            DB.Update(patient);
            TempData["editsuccess"] = "edit thành công";
            return RedirectToAction("Details", new { id = patient.Id });
        }

        //Thêm bệnh án
        [HttpPost]
        public IActionResult CreateRecord([Bind("Id,Reason,Diagnostic,Causal,Date,TreatmentName,MarrowRecord,Debit,Note,TreatmentId,UserId,Prescription")] PatientRecord patientRecord, int PatientId, string PatientName, string PatientPhone, string PatientEmail)
        {
            if (isAuth("/PatientsRecord/Create", out User user))
            {
                TempData["addsuccess"] = "thêm mới thành công";
                patientRecord.PatientId = PatientId;
                patientRecord.UserId = user.Id;
                patientRecord.Date = DateTime.Now;  
                DBRecord.Add(patientRecord);
                Log.Add(new SystemLog
                {
                    CreatedDate = DateTime.Now,
                    OwnerId = user.Id,
                    Content = "người dùng đã thêm mới bệnh án " +
                    PatientName + " có sô điện thoại là " + PatientPhone + " và email là " + PatientEmail
                });
                return Redirect("Details/" + PatientId);
            }
            else return NotFound();

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
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
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
