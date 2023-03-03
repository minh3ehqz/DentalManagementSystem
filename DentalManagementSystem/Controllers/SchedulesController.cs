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
        public IActionResult Index()
        {
            if (!isAuth(out User user))
            {
                return NotFound();
            }
            var list = DB.ListAll();
            foreach (var item in list)
            {
                if (item.Date < DateTime.Now)
                {
                    item.Status = -1;
                    DB.Update(item);
                }
            }
            return View(list);
        }

        [HttpPost]
        public IActionResult Create([Bind("PatientId,Date")] Schedule schedule)
        {
            DB.Add(schedule);
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
                Log.Add(new SystemLog { CreatedDate = DateTime.Now, OwnerId = user.Id, Content = "người dùng đã hủy lịch hẹn lúc " + DB.Get(id).Date + " của bênh nhân " + patient.Get(DB.Get(id).PatientId).Name + "" });
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
