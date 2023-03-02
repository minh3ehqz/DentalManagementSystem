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
         /*   if (!isAuth(out User user))
            {
                    return NotFound();
            }
            else*/
            {
                var checkgender = (gender ?? "");
                TempData["id"] = (id ?? null);
                TempData["name"] = (name ?? "");
                TempData["Birthday"] = (Birthday ?? "");
                TempData["address"] = (address ?? "");
                TempData["phone"] = (phone ?? "");
                TempData["email"] = (email ?? "");
                TempData["gender"] = checkgender;
                var PatientList = DB.ListAll();
                return View(PatientList);
            }
        }

        // POST: thêm mới bệnh nhân
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Birthday,Gender,Address,Phone,Email,BodyPrehistory,TeethPrehistory,Status,IsDeleted")] Patient patient)
        {
            if(isAuth(out User user))
            {
                TempData["addsuccess"] = "thêm mới thành công";
                patient.Trim();
                DB.Add(patient);
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã thêm mới bệnh nhân" });
                return RedirectToAction(nameof(Index));
            }else return NotFound();
            
        }
        // thông tin chi tiết của bệnh nhân
        public IActionResult Details(long id)
        {
            /*if (!isAuth(out User user))
            {
                return NotFound();
            }*/
            var patient = DB.Get(id);
            return View(patient);
        }
        // GET: thay đổi thông tin bênh nhân


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
          /*  if (!isAuth(out User user))
            {
                return NotFound();
            }*/
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
     //           Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã xóa bệnh nhân có id là " + id + "" });
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
