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
using System.Data;
using DentalManagementSystem.Utils;

namespace DentalManagementSystem.Controllers
{
    public class SchedulesController : AuthController
    {
        scheduleDBContext DB = new scheduleDBContext();
        SystemLogDBContext Log = new SystemLogDBContext();
        PatientDBContext patient = new PatientDBContext();


        // GET: Schedules
        public IActionResult Index(string textSearch, int status, int pageincoming = 1, int pagedone = 1, string watingActive = "show active", string NotWatingActive = " ")
        {
            if (!isAuth("/Schedules/Index",out User user))
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
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            ViewData["searchContent"] = textSearch;
            ViewData["watingActive"] = watingActive;
            ViewData["notWattingActive"] = NotWatingActive;

            int count = DB.ListAll((string)ViewData["searchContent"], 0).Count();
            ViewData["thisPageComing"] = pageincoming;
            ViewData["sttIcoming"] = pageincoming - 1;
            ViewData["numberOfPageIncoming"] = count % 10 == 0 ? (count / 10) : (count / 10) + 1;
            ViewData["wating"] = DB.ListInPage(pageincoming, (string)ViewData["searchContent"], 0);

            count = DB.ListAll((string)ViewData["searchContent"], 1).Count();
            ViewData["sttDone"] = pagedone - 1;
            ViewData["numberOfPageDone"] = count % 10 == 0 ? (count / 10) : (count / 10) + 1;
            ViewData["thisPageDone"] = pagedone;
            ViewData["notwaiting"] = DB.ListInPage(pagedone, (string)ViewData["searchContent"], 1);

            return View();
        }

        [HttpPost]
        public IActionResult Create([Bind("PatientId,Date")] Schedule schedule)
        {
            if (!isAuth("/Schedules/Create",out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            DB.Add(schedule);
            Log.Add(new SystemLog
            {
                CreatedDate = DateTime.Now,
                OwnerId = user.Id,
                Content = "người dùng đã đặt lịch hẹn lúc " + schedule.Date + " của bênh nhân " + patient.Get(schedule.PatientId).Name +
                " số điện thoại là " + patient.Get(schedule.PatientId).Phone + ""
            });
            return Redirect("~/Patients/Details/" + schedule.PatientId + "");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(long[] selectedValues)
        {
            if (!isAuth("/Schedules/Delete",out User user))
            {
                return NotFound();
            }
            TempData["Delete messenger"] = "xóa thành công";
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            foreach (long id in selectedValues)
            {
                Log.Add(new SystemLog
                {
                    CreatedDate = DateTime.Now,
                    OwnerId = user.Id,
                    Content = "người dùng đã hủy lịch hẹn lúc " + DB.Get(id).Date + " của bênh nhân " + patient.Get(DB.Get(id).PatientId).Name +
                    " số điện thoại là " + patient.Get(DB.Get(id).PatientId).Phone + ""
                });
                DB.Delete(id);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult Update([Bind("Id,Date,PatientId,Status")] Schedule schedule)
        {
            if (!isAuth("/Schedules/Delete", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            if (schedule.Date.Day == DateTime.Now.Day && schedule.Date.Month == DateTime.Now.Month)
                DB.Update(schedule);
            return RedirectToAction("Index");
        }

    }
}
