using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using System.Net;

namespace DentalManagementSystem.Controllers
{
    public class SchedulesController : AuthController
    {
        scheduleDBContext DB = new scheduleDBContext();
        SystemLogDBContext Log = new SystemLogDBContext();
        PatientDBContext patient = new PatientDBContext();


        // GET: Schedules
        public IActionResult Index(string textSearch, int status, int page = 1)
        {
            if (!isAuth(out User user))
            {
                return NotFound();
            }
            foreach (var item in DB.ListAll())
            {
                if (item.Date < DateTime.Now)
                {
                    item.Status = -1;
                    DB.Update(item);
                }
            }
            ViewData["searchContent"] = textSearch;
            int count = DB.ListAll((string)ViewData["searchContent"],status).Count();
            ViewData["thisPage"] = page;
            ViewData["stt"] = page - 1;
            ViewData["numberOfPage"] = count % 10 == 0 ? (count / 10) : (count / 10) + 1;
            var list = DB.ListInPage(page, (string)ViewData["searchContent"],status);
            ViewData["wating"] = DB.ListInPage(page, (string)ViewData["searchContent"], 0); 
            ViewData["notwaiting"] = DB.ListInPage(page, (string)ViewData["searchContent"], 1);
            return View(list);
        }

        [HttpPost]
        public IActionResult Create([Bind("PatientId,Date")] Schedule schedule)
        {
            if (!isAuth(out User user))
            {
                return NotFound();
            }
            DB.Add(schedule);
            Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã đặt lịch hẹn lúc " + schedule.Date + " của bênh nhân " + patient.Get(schedule.PatientId).Name + 
                " số điện thoại là "+ patient.Get(schedule.PatientId) .Phone+ "" });
            return Redirect("~/Patients/Details/" + schedule.PatientId + "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long[] selectedValues)
        {
            if (!isAuth(out User user))
            {
                return NotFound();
            }
            TempData["Delete messenger"] = "xóa thành công";
            foreach (long id in selectedValues)
            {
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã hủy lịch hẹn lúc " + DB.Get(id).Date + " của bênh nhân " + patient.Get(DB.Get(id).PatientId).Name + 
                    " số điện thoại là "+ patient.Get(DB.Get(id).PatientId).Phone+ "" });
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update([Bind("Id,Date,PatientId,Status")] Schedule schedule)
        {
            DB.Update(schedule);
            return RedirectToAction("Index");
        }

    }
}
