using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using System.Collections;
using Microsoft.IdentityModel.Tokens;
using DentalManagementSystem.Utils;

namespace DentalManagementSystem.Controllers
{
    public class SystemLogsController : AuthController
    {
        SystemLogDBContext DB = new SystemLogDBContext();
        // GET: SystemLogs
        public IActionResult Index(string textSearch, int page=1)
        {
            if (!isAuth("/SystemLogs/Index",out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;

            ViewData["searchContent"] = textSearch;
            int count = DB.ListAll((string)ViewData["searchContent"]).Count();
            ViewData["thisPage"] = page;
            ViewData["stt"] = page-1;
            ViewData["numberOfPage"]  = count%10==0?(count / 10) : (count / 10)+1;
            var list = DB.ListInPage(page, (string)ViewData["searchContent"]);
            return View(list);
        }

        [HttpPost]
        public IActionResult CreateReportLog(String context, String url)
        {
            if (!isAuth("/SystemLogs/Create", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            DB.Add(new SystemLog
            {
                Content = context,
                OwnerId = user.Id,
                CreatedDate = DateTime.Now,
            }) ;
            return Redirect(url);
        }

    }
}
