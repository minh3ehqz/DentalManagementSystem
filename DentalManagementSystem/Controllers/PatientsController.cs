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
        public IActionResult Index(long id,String name,String Birthday,String address,String phone,String email )
        {
            if (id != 0 || Birthday !=null|| name != null || address != null || phone != null || email != null)
            {
                var result = DB.Patients.Where(x => id != 0 && x.Id == id
                || name != null && x.Name.Contains(name)
                || Birthday !=null && x.Birthday.ToString().Contains(Birthday)
                || address != null && x.Address.Contains(address)
                || phone != null && x.Phone.Contains(phone)
                || email != null && x.Email.Contains(email)).ToList();
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

        //kiểm tra năm sinh hợp lệ
        public bool checkBirthday(DateTime birthday)
        {
            if (DateTime.Now < birthday) return false;
            else if(DateTime.Now.Year - birthday.Year>=150) return false;
            return true;
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
            if (checkBirthday(patient.Birthday))
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
            if(checkBirthday(patient.Birthday))
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
