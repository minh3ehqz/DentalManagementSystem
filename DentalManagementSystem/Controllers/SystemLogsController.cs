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
    public class SystemLogsController : AuthController
    {
        SystemLogDBContext DB = new SystemLogDBContext();

        // GET: SystemLogs
        public IActionResult Index()
        {
            if (!isAuth(out User user))
            {
                return NotFound();
            }
            var list = DB.ListAll();
            return View(list);
        }

        [HttpPost]
        public IActionResult CreateReportLog(String context, String url)
        {
            return Redirect(url);
        }

    }
}
