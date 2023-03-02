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

        

        // GET: Schedules
        public IActionResult Index()
        {
            /*if (!isAuth(out User user))
            {
                return NotFound();
            }
            else*/
            {
                var list = DB.ListAll();
                foreach(var item in list)
                {
                    if (item.Date < DateTime.Now)
                    {
                        item.Status = -1;
                    }
                }
                return View(list);
            }
            
        }

        [HttpPost]
        public IActionResult Create([Bind("PatientId,Date")]Schedule schedule)
        {
            DB.Add(schedule);
            return Redirect("~/Patients/Details/"+schedule.PatientId+"");
        }

      
    }
}
