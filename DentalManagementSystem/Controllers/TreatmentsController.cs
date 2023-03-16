using DentalManagementSystem.DAL;
using DentalManagementSystem.Models;
using DentalManagementSystem.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DentalManagementSystem.Controllers
{
    public class TreatmentsController : AuthController
    {
        TreatmentsDBContext DB = new TreatmentsDBContext();
        SystemLogDBContext Log = new SystemLogDBContext();

        [HttpPost]
        public IActionResult Create([Bind("PatientId","Name")]Treatment treatment)
        {
            if (!isAuth("/Treatments/Create", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            DB.Add(treatment);
            Log.Add(new SystemLog
            {
                CreatedDate = DateTime.Now,
                OwnerId = user.Id,
                Content = "người dùng đã thêm pháp đồ điều trị cho bênh nhân có id là" +
                    "" + treatment.PatientId
            });
            return Redirect("/Patients/Details/" + treatment.PatientId);
        }

        [HttpPost]
        public IActionResult Delete(long id ,long patientid)
        {
            if (!isAuth("/Treatments/Delete", out User user))
            {
                return NotFound();
            }
            ViewData["FullName"] = user.FullName;
            ViewData["Role"] = RoleHelper.GetRoleNameById(user.RoleId);
            ViewData["Email"] = user.Email;
            DB.Delete(id);
            Log.Add(new SystemLog
            {
                CreatedDate = DateTime.Now,
                OwnerId = user.Id,
                Content = "người dùng đã xóa pháp đồ điều trị của bệnh nhân có id là "+patientid
            });
            return Redirect("/Patients/Details/" + patientid);
        }
    }
}
